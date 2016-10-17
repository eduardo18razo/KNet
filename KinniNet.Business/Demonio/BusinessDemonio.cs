using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
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

                DateTime fechaFin = DateTime.Now;
                DateTime fechaInicio = fechaFin;
                List<TiempoInformeArbol> j =
                    db.TiempoInformeArbol.Join(db.Ticket, tia => tia.IdArbol, t => t.IdArbolAcceso, (tia, t) => new { tia, t })
                        .Where(@t1 => @t1.t.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto)
                        .Select(@t1 => @t1.tia).Distinct().ToList();
                List<Ticket> informeDueño = new List<Ticket>();
                List<Ticket> informeMantenimiento = new List<Ticket>();
                List<Ticket> informeDesarrollo = new List<Ticket>();
                List<Ticket> informeConsulta = new List<Ticket>();
                foreach (TiempoInformeArbol informeArbol in j)
                {
                    fechaInicio = fechaFin.AddDays(-double.Parse(informeArbol.TiempoNotificacion.ToString()));
                    List<Ticket> selectTickets = db.TiempoInformeArbol.Join(db.Ticket, tia => tia.IdArbol,
                        t => t.IdArbolAcceso, (tia, t) => new { tia, t })
                        .Where(@t1 => @t1.t.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto
                                && @t1.t.FechaHoraFinProceso >= fechaInicio && @t1.t.FechaHoraFinProceso <= fechaFin)
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
                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                            informeDueño.AddRange(selectTickets.ToList().Distinct());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada:
                            informeMantenimiento.AddRange(selectTickets.ToList().Distinct());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                            informeDesarrollo.AddRange(selectTickets.ToList().Distinct());
                            break;
                        case (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta:
                            informeConsulta.AddRange(selectTickets.ToList().Distinct());
                            break;
                    }
                }
                informeDueño = informeDueño.Distinct().ToList();
                informeMantenimiento = informeMantenimiento.Distinct().ToList();
                informeDesarrollo = informeDesarrollo.Distinct().ToList();
                informeConsulta = informeConsulta.Distinct().ToList();

                EnviaNotificacion(informeDueño, (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención);
                EnviaNotificacion(informeMantenimiento, (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada);
                EnviaNotificacion(informeDesarrollo, (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo);
                EnviaNotificacion(informeConsulta, (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta);

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
                            SendMail(correoUsuario.Correo,
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

        private void SendMail(string addressTo, string subject, string content)
        {
            try
            {
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                MailAddress fromAddress = new MailAddress(section.From, "Eduardo Cerritos");
                MailAddress toAddress = new MailAddress(addressTo, "Prueba");

                var smtp = new SmtpClient
                {
                    Host = section.Network.Host,//"smtp.gmail.com",
                    Port = section.Network.Port,
                    EnableSsl = section.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = section.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, section.Network.Password)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = content
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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
