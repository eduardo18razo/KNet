using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Tickets;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Demonio
{
    public class BusinessDemonio : IDisposable
    {
        private readonly bool _proxy;

        public void Dispose()
        {

        }

        public BusinessDemonio(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void EnvioNotificacion()
        {

            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                DateTime fechaFin = DateTime.Now;
                List<TiempoInformeArbol> j = db.TiempoInformeArbol.Join(db.Ticket, tia => tia.IdArbol, t => t.IdArbolAcceso, (tia, t) => new { tia, t })
                        .Where(@t1 => @t1.t.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto).Select(@t1 => @t1.tia).Distinct().ToList();
                List<Ticket> informeDueño = new List<Ticket>();
                List<Ticket> informeMantenimiento = new List<Ticket>();
                List<Ticket> informeDesarrollo = new List<Ticket>();
                List<Ticket> informeConsulta = new List<Ticket>();
                Dictionary<int, List<Ticket>> dictionaryInformeDueño = new Dictionary<int, List<Ticket>>();
                foreach (TiempoInformeArbol informeArbol in j)
                {
                    DateTime fechaInicio = fechaFin.AddDays(-double.Parse(informeArbol.TiempoNotificacion.ToString()));
                    List<Ticket> selectTickets = db.TiempoInformeArbol.Join(db.Ticket, tia => tia.IdArbol, t => t.IdArbolAcceso, (tia, t) => new { tia, t })
                        .Where(@t1 => @t1.t.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto && @t1.t.FechaHoraFinProceso >= fechaInicio && @t1.t.FechaHoraFinProceso <= fechaFin)
                        .Select(@t1 => @t1.t).Distinct().ToList();
                    foreach (Ticket ticket in selectTickets)
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "TicketGrupoUsuario");

                        foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario)
                        {
                            db.LoadProperty(tgu, "GrupoUsuario");
                            db.LoadProperty(tgu.GrupoUsuario, "UsuarioGrupo");
                            foreach (UsuarioGrupo ug in tgu.GrupoUsuario.UsuarioGrupo)
                            {
                                db.LoadProperty(ug, "Usuario");
                                db.LoadProperty(ug.Usuario, "CorreoUsuario");
                            }
                        }
                    }
                    switch (informeArbol.IdTipoGrupo)
                    {
                        case (int)BusinessVariables.EnumTiposGrupos.Agente:
                            informeDueño.AddRange(selectTickets.ToList().Distinct());
                            dictionaryInformeDueño.Add(informeArbol.IdTipoNotificacion, selectTickets.Distinct().ToList());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeContenido:
                            informeMantenimiento.AddRange(selectTickets.ToList().Distinct());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                            informeDesarrollo.AddRange(selectTickets.ToList().Distinct());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.ConsultasEspeciales:
                            informeConsulta.AddRange(selectTickets.ToList().Distinct());
                            break;
                    }
                }
                informeDueño = informeDueño.Distinct().ToList();
                informeMantenimiento = informeMantenimiento.Distinct().ToList();
                informeDesarrollo = informeDesarrollo.Distinct().ToList();
                informeConsulta = informeConsulta.Distinct().ToList();

                EnviaNotificacion(informeDueño, (int)BusinessVariables.EnumTiposGrupos.Agente);
                EnviaNotificacion(informeMantenimiento, (int)BusinessVariables.EnumTiposGrupos.ResponsableDeContenido);
                EnviaNotificacion(informeDesarrollo, (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo);
                EnviaNotificacion(informeConsulta, (int)BusinessVariables.EnumTiposGrupos.ConsultasEspeciales);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void Enviarnotificacion(Dictionary<int, List<Ticket>> informeConsulta, int idTipoGrupo)
        {
            try
            {
                foreach (KeyValuePair<int, List<Ticket>> valuePair in informeConsulta)
                {
                    foreach (Ticket ticket in valuePair.Value)
                    {
                        foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario.Where(w => w.GrupoUsuario.IdTipoGrupo == idTipoGrupo).Distinct())
                        {
                            foreach (UsuarioGrupo ug in tgu.GrupoUsuario.UsuarioGrupo)
                            {

                                switch (valuePair.Key)
                                {
                                    case 1:
                                        foreach (CorreoUsuario correoUsuario in ug.Usuario.CorreoUsuario)
                                        {
                                            EnviaCorreo(correoUsuario.Correo, correoUsuario.Usuario.NombreCompleto, ticket, tgu.GrupoUsuario.Descripcion);
                                        }

                                        break;
                                    case 2:
                                        foreach (TelefonoUsuario telefono in ug.Usuario.TelefonoUsuario.Where(w => w.IdTipoTelefono == (int)BusinessVariables.EnumTipoTelefono.Celular))
                                        {
                                            EnviaCorreo(telefono.Numero, telefono.Usuario.NombreCompleto, ticket, tgu.GrupoUsuario.Descripcion);
                                        }
                                        break;
                                    case 3:
                                        break;
                                    case 4:
                                        break;
                                    case 5:
                                        break;
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void EnviaNotificacion(List<Ticket> informeConsulta, int idTipoGrupo)
        {
            foreach (Ticket ticket in informeConsulta)
            {
                foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario.Where(w => w.GrupoUsuario.IdTipoGrupo == idTipoGrupo).Distinct())
                {
                    foreach (UsuarioGrupo ug in tgu.GrupoUsuario.UsuarioGrupo)
                    {
                        foreach (CorreoUsuario correoUsuario in ug.Usuario.CorreoUsuario)
                        {
                            BusinessCorreo.SendMail(correoUsuario.Correo,
                                string.Format("Ticket {0} Clave Registro {1} {2}", ticket.Id, ticket.Random ? ticket.ClaveRegistro : "N/A", tgu.GrupoUsuario.Descripcion),
                                string.Format("Grupo {0} " +
                                              "<br>Persona {1} " +
                                              "<br>Persona Levanto {2}" +
                                              "<br>Ticket Tiempo que levanto {3} " +
                                              "<br>tiempo envio {4}",
                                              tgu.GrupoUsuario.Descripcion,
                                              correoUsuario.Usuario.NombreCompleto,
                                              ticket.UsuarioLevanto.NombreCompleto,
                                              ticket.FechaHoraAlta,
                                              ticket.FechaHoraFinProceso));
                        }
                    }
                }
            }
        }

        private void EnviaCorreo(string correo, string nombreCompleto, Ticket ticket, string grupo)
        {
            try
            {
                BusinessCorreo.SendMail(correo, string.Format("Ticket {0} Clave Registro {1} {2}", ticket.Id, ticket.Random ? ticket.ClaveRegistro : "N/A", grupo),
                                        string.Format("Grupo {0} " + "<br>Persona {1} " + "<br>Persona Levanto {2}" + "<br>Ticket Tiempo que levanto {3} " + "<br>tiempo envio {4}",
                                                      grupo, nombreCompleto, ticket.UsuarioLevanto.NombreCompleto, ticket.FechaHoraAlta, ticket.FechaHoraFinProceso));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void EnviaSms(string numero, string nombreCompleto, Ticket ticket, string grupo)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizaSla()
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var y = db.Ticket.Where(w => w.DentroSla && w.FechaTermino == null);
                foreach (Ticket ticket in y)
                {
                    ticket.DentroSla = DateTime.Now <= ticket.FechaHoraFinProceso;
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
