﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Tickets;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessTicket : IDisposable
    {
        private readonly bool _proxy;

        public void Dispose()
        {

        }

        public BusinessTicket(bool proxy = false)
        {
            _proxy = proxy;
        }

        private DateTime TiempoGeneral(List<HorarioSubGrupo> horarioSubGrupo, decimal? tiempoProceso)
        {
            DateTime result;
            try
            {
                List<DateTime> diasAsignados = new List<DateTime>();
                string horarioInicio = horarioSubGrupo.Min(s => s.HoraInicio);
                string horarioFin = horarioSubGrupo.Max(s => s.HoraFin);
                double tiempotrabajo = double.Parse(horarioFin.Replace(':', '.').Substring(0, 5)) - double.Parse(horarioInicio.Replace(':', '.').Substring(0, 5));

                decimal? horasTotalSolucion = tiempoProceso;
                int contador = 0;
                while (horasTotalSolucion > 0)
                {
                    if (horarioSubGrupo.Any(a => a.Dia == (int)DateTime.Now.AddDays(contador).DayOfWeek))
                    {
                        horarioInicio = horarioSubGrupo.Where(w => w.Dia == (int)DateTime.Now.AddDays(contador).DayOfWeek).Min(m => m.HoraInicio);
                        horarioFin = horarioSubGrupo.Where(w => w.Dia == (int)DateTime.Now.AddDays(contador).DayOfWeek).Max(m => m.HoraFin);
                        if (diasAsignados.Count <= 0)
                        {
                            horasTotalSolucion -= decimal.Parse(Math.Round((DateTime.Parse(DateTime.Now.ToShortDateString() + " " + horarioFin) - DateTime.Now).TotalHours, 2, MidpointRounding.ToEven).ToString());
                            diasAsignados.Add(DateTime.Now.AddDays(contador));
                        }
                        else
                        {
                            if (horasTotalSolucion >= decimal.Parse(tiempotrabajo.ToString()))
                            {
                                horasTotalSolucion -= decimal.Parse(tiempotrabajo.ToString());
                                diasAsignados.Add(DateTime.Now.AddDays(contador));
                            }
                            else
                            {
                                DateTime fecha = DateTime.Parse(DateTime.Now.AddDays(contador).ToShortDateString() + " " + horarioInicio).AddHours(double.Parse(horasTotalSolucion.ToString()));
                                horasTotalSolucion -= horasTotalSolucion;
                                diasAsignados.Add(fecha);
                            }
                        }
                    }
                    contador++;
                }
                if (tiempoProceso == 0)
                    diasAsignados.Add(DateTime.Now);
                result = DateTime.ParseExact(diasAsignados.Max().ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public Ticket CrearTicket(int idUsuario, int idUsuarioSolicito, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura, int idCanal, bool campoRandom, bool esTercero)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            Ticket result;
            try
            {
                Usuario usuario = new BusinessUsuarios().ObtenerUsuario(idUsuario);
                ArbolAcceso arbol = new BusinessArbolAcceso().ObtenerArbolAcceso(idArbol);
                Mascara mascara = new BusinessMascaras().ObtenerMascaraCaptura(arbol.InventarioArbolAcceso.First().IdMascara ?? 0);
                Encuesta encuesta = new BusinessEncuesta().ObtenerEncuestaById(arbol.InventarioArbolAcceso.First().IdEncuesta ?? 0);
                Sla sla = new BusinessSla().ObtenerSla(arbol.InventarioArbolAcceso.First().IdSla ?? 0);
                Ticket ticket = new Ticket
                {
                    IdTipoUsuario = usuario.IdTipoUsuario,
                    IdTipoArbolAcceso = arbol.IdTipoArbolAcceso,
                    IdArbolAcceso = arbol.Id,
                    IdImpacto = (int)arbol.IdImpacto,
                    IdUsuarioLevanto = usuario.Id,
                    IdUsuarioSolicito = idUsuarioSolicito,
                    IdOrganizacion = usuario.IdOrganizacion,
                    IdUbicacion = usuario.IdUbicacion,
                    IdMascara = mascara.Id,
                    IdEncuesta = encuesta.Id,
                    IdCanal = idCanal,
                    //RespuestaEncuesta = new List<RespuestaEncuesta>(),
                    IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto,
                    FechaHoraAlta = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar,
                    Random = campoRandom,
                    ClaveRegistro = GeneraCampoRandom(),
                    EsTercero = usuario.Id != idUsuarioSolicito,
                };
                //ENCUESTA
                //ticket.RespuestaEncuesta.AddRange(encuesta.EncuestaPregunta.Select(pregunta => new RespuestaEncuesta { IdEncuesta = encuesta.Id, IdPregunta = pregunta.Id }));
                //GrupoUsuario USUARIO
                ticket.TicketGrupoUsuario = new List<TicketGrupoUsuario>();
                foreach (GrupoUsuarioInventarioArbol grupoArbol in arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                {
                    TicketGrupoUsuario grupo = new TicketGrupoUsuario { IdGrupoUsuario = grupoArbol.IdGrupoUsuario };
                    if (grupoArbol.IdSubGrupoUsuario != null)
                        grupo.IdSubGrupoUsuario = grupoArbol.IdSubGrupoUsuario;
                    ticket.TicketGrupoUsuario.Add(grupo);
                }

                //SLA
                ticket.SlaEstimadoTicket = new SlaEstimadoTicket
                {
                    FechaInicio = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    Dias = sla.Dias,
                    Horas = sla.Horas,
                    Minutos = sla.Minutos,
                    Segundos = sla.Segundos,
                    FechaInicioProceso = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    TiempoHoraProceso = sla.TiempoHoraProceso,
                    Terminado = false,
                    SlaEstimadoTicketDetalle = new List<SlaEstimadoTicketDetalle>()
                };

                List<HorarioSubGrupo> lstHorarioGrupo = new List<HorarioSubGrupo>();
                List<DiaFestivoSubGrupo> lstDiasFestivosGrupo = new List<DiaFestivoSubGrupo>();
                foreach (SubGrupoUsuario sGpoUsuario in ticket.TicketGrupoUsuario.SelectMany(tgrupoUsuario => db.GrupoUsuario.Where(w => w.Id == tgrupoUsuario.IdGrupoUsuario && w.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).SelectMany(gpoUsuario => gpoUsuario.SubGrupoUsuario)))
                {
                    lstHorarioGrupo.AddRange(db.HorarioSubGrupo.Where(w => w.IdSubGrupoUsuario == sGpoUsuario.Id).ToList());
                    lstDiasFestivosGrupo.AddRange(db.DiaFestivoSubGrupo.Where(w => w.IdSubGrupoUsuario == sGpoUsuario.Id));
                }
                DateTime fechaTermino = TiempoGeneral(lstHorarioGrupo, ticket.SlaEstimadoTicket.TiempoHoraProceso);
                ticket.FechaHoraFinProceso = fechaTermino;
                ticket.SlaEstimadoTicket.FechaFinProceso = fechaTermino;
                ticket.SlaEstimadoTicket.FechaFin = fechaTermino;

                //SLA DETALLE
                ticket.SlaEstimadoTicket.SlaEstimadoTicketDetalle.AddRange(
                    sla.SlaDetalle.Select(
                        detalle =>
                            new SlaEstimadoTicketDetalle
                            {
                                IdSubRol = detalle.IdSubRol,
                                Dias = sla.Dias,
                                Horas = sla.Horas,
                                Minutos = sla.Minutos,
                                Segundos = sla.Segundos,
                                TiempoProceso = detalle.TiempoProceso
                            }));

                ticket.IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto;
                ticket.TicketEstatus = new List<TicketEstatus>
                {
                    new TicketEstatus
                    {
                        IdEstatus = ticket.IdEstatusTicket,
                        IdUsuarioMovimiento = idUsuario,
                        FechaMovimiento =
                            DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                                "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    }
                };
                ticket.TicketAsignacion = new List<TicketAsignacion>
                {
                    new TicketAsignacion
                    {
                        IdEstatusAsignacion =
                            (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar,
                        FechaAsignacion =
                            DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                                "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    }
                };

                db.Ticket.AddObject(ticket);
                db.SaveChanges();
                string store = string.Format("{0} '{1}',", mascara.ComandoInsertar, ticket.Id);
                store = lstCaptura.Aggregate(store,
                    (current, captura) => current + string.Format("'{0}',", captura.Valor));
                store = store.Trim().TrimEnd(',');
                if (ticket.Random)
                    store = store + ", '" + ticket.ClaveRegistro + "'";
                db.ExecuteStoreCommand(store);
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = new Ticket { Id = ticket.Id, Random = campoRandom, ClaveRegistro = ticket.ClaveRegistro };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        private string GeneraCampoRandom()
        {
            string result = null;
            try
            {
                Random obj = new Random();
                int longitud = BusinessVariables.ParametrosMascaraCaptura.CaracteresCampoRandom.Length;
                for (int i = 0; i < BusinessVariables.ParametrosMascaraCaptura.LongitudRandom; i++)
                {
                    result += BusinessVariables.ParametrosMascaraCaptura.CaracteresCampoRandom[obj.Next(longitud)];
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public void CambiarEstatus(int idTicket, int idEstatus, int idUsuario, string comentario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket);

                if (ticket != null)
                {
                    db.LoadProperty(ticket, "TicketGrupoUsuario");
                    if (ticket.IdEstatusTicket == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera)
                    {
                        if (idEstatus == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera)
                            throw new Exception("Ticket ya se encuentra en espera");

                        db.LoadProperty(ticket, "SlaEstimadoTicket");
                        List<HorarioSubGrupo> lstHorarioGrupo = new List<HorarioSubGrupo>();
                        List<DiaFestivoSubGrupo> lstDiasFestivosGrupo = new List<DiaFestivoSubGrupo>();
                        foreach (SubGrupoUsuario sGpoUsuario in ticket.TicketGrupoUsuario.SelectMany(
                            tgrupoUsuario => db.GrupoUsuario.Where(w => w.Id == tgrupoUsuario.IdGrupoUsuario && w.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención)
                                .SelectMany(gpoUsuario => gpoUsuario.SubGrupoUsuario)))
                        {
                            lstHorarioGrupo.AddRange(db.HorarioSubGrupo.Where(w => w.IdSubGrupoUsuario == sGpoUsuario.Id).ToList());
                            lstDiasFestivosGrupo.AddRange(db.DiaFestivoSubGrupo.Where(w => w.IdSubGrupoUsuario == sGpoUsuario.Id));
                        }

                        ticket.FechaFinEspera = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                        if (ticket.FechaInicioEspera != null)
                        {
                            DateTime oldDate = (DateTime)ticket.FechaInicioEspera;
                            DateTime newDate = (DateTime)ticket.FechaFinEspera;
                            TimeSpan ts = newDate - oldDate;
                            ticket.TiempoEspera += ticket.TiempoEspera == null ? 0 + ts.TotalHours : double.Parse(ticket.TiempoEspera) + ts.TotalHours;
                            ticket.FechaHoraFinProceso = TiempoGeneral(lstHorarioGrupo, ticket.SlaEstimadoTicket.TiempoHoraProceso).AddHours(ts.TotalHours);
                        }
                    }

                    ticket.TicketEstatus = new List<TicketEstatus>{new TicketEstatus
                    {
                        FechaMovimiento =  DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",
                            CultureInfo.InvariantCulture),
                            IdEstatus = idEstatus,
                            IdUsuarioMovimiento = idUsuario,
                            Comentarios = comentario.Trim().ToUpper()
                    }};
                    if (idEstatus == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto)
                    {
                        ticket.IdUsuarioResolvio = idUsuario;
                        ticket.TicketAsignacion = new List<TicketAsignacion>
                        {
                            new TicketAsignacion
                            {
                                IdEstatusAsignacion =
                                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Asignado,
                                IdUsuarioAsigno = idUsuario,
                                IdUsuarioAsignado = ticket.IdUsuarioLevanto,
                                FechaAsignacion =
                                    DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                                        "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                                Comentarios = comentario.Trim().ToUpper()
                            }
                        };
                    }
                    if (idEstatus == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto)
                    {
                        ticket.IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar;
                        ticket.TicketAsignacion = new List<TicketAsignacion>
                        {
                            new TicketAsignacion
                            {
                                IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar,
                                IdUsuarioAsigno = idUsuario,
                                IdUsuarioAsignado = null,
                                FechaAsignacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                                Comentarios = comentario.Trim().ToUpper()
                            }
                        };
                    }
                    if (idEstatus == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado)
                    {
                        ticket.FechaTermino = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                        ticket.IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar;
                        ticket.TicketAsignacion = new List<TicketAsignacion>
                        {
                            new TicketAsignacion
                            {
                                IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar,
                                IdUsuarioAsigno = idUsuario,
                                IdUsuarioAsignado = null,
                                FechaAsignacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                                Comentarios = comentario.Trim().ToUpper()
                            }
                        };
                    }
                    if (idEstatus == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera)
                    {
                        ticket.Espera = true;
                        ticket.FechaInicioEspera = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                        ticket.FechaFinEspera = null;
                    }
                    ticket.IdEstatusTicket = idEstatus;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public void AutoAsignarTicket(int idTicket, int idUsuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket);
                if (ticket != null)
                {
                    ticket.IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado;
                    ticket.TicketAsignacion = new List<TicketAsignacion>{new TicketAsignacion
                    {
                        FechaAsignacion =  DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",CultureInfo.InvariantCulture),
                        IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado,
                        IdUsuarioAsignado = idUsuario,
                        IdUsuarioAsigno = idUsuario,
                        IdTicket = idTicket
                    }};
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna, string comentario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket);
                if (ticket != null)
                {
                    ticket.IdEstatusAsignacion = idEstatusAsignacion;
                    ticket.TicketAsignacion = new List<TicketAsignacion>{new TicketAsignacion
                    {
                        FechaAsignacion =  DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",CultureInfo.InvariantCulture),
                        IdEstatusAsignacion = idEstatusAsignacion,
                        IdUsuarioAsignado = idUsuarioAsignado,
                        IdUsuarioAsigno = idUsuarioAsigna,
                        IdTicket = idTicket, 
                        Comentarios = comentario.Trim().ToUpper()
                    }};
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public List<HelperTickets> ObtenerTicketsUsuario(int idUsuario, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperTickets> result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                List<Ticket> lstTickets = db.Ticket.Where(w => w.IdUsuarioLevanto == idUsuario).ToList();

                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);


                int totalRegistros = lstTickets.Count;
                //TODO: Actualizar propiedades faltantes de asignacion
                if (totalRegistros > 0)
                {
                    result = new List<HelperTickets>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "EstatusTicket");
                        db.LoadProperty(ticket, "EstatusAsignacion");
                        db.LoadProperty(ticket, "TicketEstatus");
                        db.LoadProperty(ticket, "TicketAsignacion");
                        db.LoadProperty(ticket, "Impacto");
                        foreach (TicketAsignacion asignacion in ticket.TicketAsignacion)
                        {

                            db.LoadProperty(asignacion, "UsuarioAsignado");
                            if (asignacion.UsuarioAsignado != null)
                            {
                                db.LoadProperty(asignacion.UsuarioAsignado, "UsuarioGrupo");
                                foreach (UsuarioGrupo grupo in asignacion.UsuarioAsignado.UsuarioGrupo)
                                {
                                    db.LoadProperty(grupo, "SubGrupoUsuario");
                                    if (grupo.SubGrupoUsuario != null)
                                        db.LoadProperty(grupo.SubGrupoUsuario, "SubRol");
                                }
                            }
                        }
                        db.LoadProperty(ticket, "ArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        string nivelAsignado = string.Empty;

                        HelperTickets hticket = new HelperTickets();
                        hticket.IdTicket = ticket.Id;
                        hticket.IdUsuario = ticket.IdUsuarioLevanto;
                        hticket.IdGrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Id;
                        hticket.FechaHora = ticket.FechaHoraAlta;
                        hticket.NumeroTicket = ticket.Id;
                        hticket.NombreUsuario = ticket.UsuarioLevanto.NombreCompleto;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.GrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Descripcion;
                        hticket.EstatusTicket = ticket.EstatusTicket;
                        hticket.EstatusAsignacion = ticket.EstatusAsignacion;
                        hticket.IdUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id : 0;
                        hticket.UsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto : "";
                        hticket.NivelUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Aggregate(nivelAsignado, (current, usuarioAsignado) => current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion) : "";
                        hticket.EsPropietario = ticket.IdEstatusAsignacion == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar && supervisor ? true : idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado;
                        hticket.CambiaEstatus = hticket.IdUsuarioAsignado == idUsuario;
                        hticket.Asigna = ticket.IdEstatusAsignacion == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar && supervisor ? true : idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado && ticket.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto;
                        hticket.Total = totalRegistros;
                        hticket.IdImpacto = ticket.IdImpacto;
                        hticket.Impacto = ticket.Impacto.Descripcion;
                        result.Add(hticket);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }
        public List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperTickets> result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                List<Ticket> lstTickets = new List<Ticket>();
                bool grupoConSupervisor = (db.GrupoUsuario.Join(db.UsuarioGrupo, gu => gu.Id, ug => ug.IdGrupoUsuario,
                    (gu, ug) => new { gu, ug }).Where(@t => @t.ug.IdUsuario == idUsuario).Select(@t => @t.gu)).Any(a => a.TieneSupervisor);

                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);

                List<int?> lstEstatusPermitidos = new List<int?>();
                List<int> lstGrupos = db.UsuarioGrupo.Where(ug => ug.IdUsuario == idUsuario && ug.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Select(s => s.IdGrupoUsuario).Distinct().ToList();

                if (lstGrupos.Count <= 0)
                {
                    lstGrupos = db.UsuarioGrupo.Where(ug => ug.IdUsuario == idUsuario && ug.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.Usuario).Select(s => s.IdGrupoUsuario).Distinct().ToList();
                    foreach (int idGrupo in lstGrupos)
                    {
                        lstEstatusPermitidos.AddRange((from etsrg in db.EstatusTicketSubRolGeneral
                                                       join gu in db.GrupoUsuario on new { gpo = etsrg.IdGrupoUsuario, sup = (bool)etsrg.TieneSupervisor } equals new { gpo = gu.Id, sup = gu.TieneSupervisor }
                                                       join sgu in db.SubGrupoUsuario on new { Gpo = gu.Id, gpoIn = etsrg.IdGrupoUsuario, sbr = (int)etsrg.IdSubRolSolicita } equals new { Gpo = sgu.IdGrupoUsuario, gpoIn = sgu.IdGrupoUsuario, sbr = sgu.IdSubRol }
                                                       join ug in db.UsuarioGrupo on new { idGpo = gu.Id, rol = etsrg.IdRolSolicita, dbgpo = sgu.Id } equals new { idGpo = ug.IdGrupoUsuario, rol = ug.IdRol, dbgpo = (int)ug.IdSubGrupoUsuario }
                                                       where gu.Id == idGrupo && ug.IdUsuario == idUsuario && etsrg.Habilitado
                                                       select etsrg.IdEstatusTicketActual).Distinct().ToList());
                    }
                    foreach (int grupo in lstGrupos)
                    {
                        foreach (int? estatusPermitido in lstEstatusPermitidos)
                        {
                            if (estatusPermitido == null)

                                lstTickets.AddRange(db.Ticket.Join(db.TicketAsignacion.OrderByDescending(o => o.Id).Take(1), t => t.Id, ta => ta.IdTicket, (t, ta) => new { t, ta })
                                        .Join(db.TicketGrupoUsuario, @t1 => @t1.t.Id, tgu => tgu.IdTicket, (@t1, tgu) => new { @t1, tgu })
                                        .Where(@t1 => @t1.tgu.IdGrupoUsuario == grupo && @t1.@t1.ta.IdUsuarioAsignado == null && @t1.t1.t.IdEstatusAsignacion == estatusPermitido)
                                        .Select(@t1 => @t1.@t1.t).Distinct().ToList());
                            else
                            {
                                lstTickets.AddRange(db.Ticket.Join(db.TicketAsignacion.OrderByDescending(o => o.Id).Take(1), t => t.Id, ta => ta.IdTicket, (t, ta) => new { t, ta })
                                        .Join(db.TicketGrupoUsuario, @t1 => @t1.t.Id, tgu => tgu.IdTicket, (@t1, tgu) => new { @t1, tgu })
                                        .Where(@t1 => @t1.tgu.IdGrupoUsuario == grupo && @t1.t1.t.IdEstatusTicket == estatusPermitido && @t1.t1.ta.IdUsuarioAsignado == idUsuario)
                                        .Select(@t1 => @t1.@t1.t).Distinct().ToList());
                            }
                        }
                    }
                }
                else
                {
                    //foreach (int idGrupo in lstGrupos)
                    //{
                    //    lstEstatusPermitidos.AddRange((db.EstatusAsignacionSubRolGeneral.Join(db.GrupoUsuario, easrg => easrg.IdGrupoUsuario, gu => gu.Id, (easrg, gu) => new { easrg, gu })
                    //            .Join(db.UsuarioGrupo, @t => @t.gu.Id, ug => ug.IdGrupoUsuario, (@t, ug) => new { @t, ug })
                    //            .Where(@t => @t.ug.IdGrupoUsuario == idGrupo && @t.@t.easrg.TieneSupervisor == @t.@t.gu.TieneSupervisor && @t.@t.easrg.Habilitado && @t.ug.IdUsuario == idUsuario) //&& @t.@t.easrg.Propietario
                    //            .Select(@t => (int?)@t.@t.easrg.IdEstatusAsignacionActual)).Distinct().ToList());
                    //}
                    foreach (int idGrupo in lstGrupos)
                    {
                        lstEstatusPermitidos.AddRange((from etsrg in db.EstatusTicketSubRolGeneral
                                                       join gu in db.GrupoUsuario on new { gpo = etsrg.IdGrupoUsuario, sup = (bool)etsrg.TieneSupervisor } equals new { gpo = gu.Id, sup = gu.TieneSupervisor }
                                                       join sgu in db.SubGrupoUsuario on new { Gpo = gu.Id, gpoIn = etsrg.IdGrupoUsuario, sbr = (int)etsrg.IdSubRolSolicita } equals new { Gpo = sgu.IdGrupoUsuario, gpoIn = sgu.IdGrupoUsuario, sbr = sgu.IdSubRol }
                                                       join ug in db.UsuarioGrupo on new { idGpo = gu.Id, rol = etsrg.IdRolSolicita, dbgpo = sgu.Id } equals new { idGpo = ug.IdGrupoUsuario, rol = ug.IdRol, dbgpo = (int)ug.IdSubGrupoUsuario }
                                                       where gu.Id == idGrupo && ug.IdUsuario == idUsuario && etsrg.Habilitado
                                                       select etsrg.IdEstatusTicketActual).Distinct().ToList());
                    }
                    foreach (int grupo in lstGrupos)
                    {

                        foreach (int? estatusPermitido in lstEstatusPermitidos)
                        {
                            if (supervisor || lstEstatusPermitidos.Contains((int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado))
                                lstTickets.AddRange(db.Ticket.Join(db.TicketGrupoUsuario, t => t.Id, tg => tg.IdTicket, (t, tg) => new { t, tg })
                                        .Where(@t1 => @t1.tg.IdGrupoUsuario == grupo && @t1.t.IdEstatusAsignacion == estatusPermitido)
                                        .Select(@t1 => @t1.t).Distinct().ToList());
                            else if (estatusPermitido == null)
                                lstTickets.AddRange(db.Ticket.Join(db.TicketAsignacion.OrderByDescending(o => o.Id).Take(1), t => t.Id, ta => ta.IdTicket, (t, ta) => new { t, ta })
                                        .Join(db.TicketGrupoUsuario, @t1 => @t1.t.Id, tgu => tgu.IdTicket, (@t1, tgu) => new { @t1, tgu })
                                        .Where(@t1 => @t1.tgu.IdGrupoUsuario == grupo && @t1.@t1.ta.IdUsuarioAsignado == null && @t1.t1.t.IdEstatusAsignacion == estatusPermitido)
                                        .Select(@t1 => @t1.@t1.t).Distinct().ToList());
                            else
                            {
                                lstTickets.AddRange(db.Ticket.Join(db.TicketAsignacion.OrderByDescending(o => o.Id).Take(1), t => t.Id, ta => ta.IdTicket, (t, ta) => new { t, ta })
                                        .Join(db.TicketGrupoUsuario, @t1 => @t1.t.Id, tgu => tgu.IdTicket, (@t1, tgu) => new { @t1, tgu })
                                        .Where(@t1 => @t1.tgu.IdGrupoUsuario == grupo && @t1.@t1.ta.IdUsuarioAsignado == idUsuario && @t1.t1.t.IdEstatusAsignacion == estatusPermitido && @t1.t1.ta.IdUsuarioAsignado == idUsuario)
                                        .Select(@t1 => @t1.@t1.t).Distinct().ToList());
                            }
                        }
                    }
                }
                int totalRegistros = lstTickets.Count;
                //TODO: Actualizar propiedades faltantes de asignacion
                if (totalRegistros > 0)
                {
                    result = new List<HelperTickets>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "EstatusTicket");
                        db.LoadProperty(ticket, "EstatusAsignacion");
                        db.LoadProperty(ticket, "TicketEstatus");
                        db.LoadProperty(ticket, "TicketAsignacion");
                        db.LoadProperty(ticket, "Impacto");
                        foreach (TicketAsignacion asignacion in ticket.TicketAsignacion)
                        {

                            db.LoadProperty(asignacion, "UsuarioAsignado");
                            if (asignacion.UsuarioAsignado != null)
                            {
                                db.LoadProperty(asignacion.UsuarioAsignado, "UsuarioGrupo");
                                foreach (UsuarioGrupo grupo in asignacion.UsuarioAsignado.UsuarioGrupo)
                                {
                                    db.LoadProperty(grupo, "SubGrupoUsuario");
                                    if (grupo.SubGrupoUsuario != null)
                                        db.LoadProperty(grupo.SubGrupoUsuario, "SubRol");
                                }
                            }
                        }
                        db.LoadProperty(ticket, "ArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        string nivelAsignado = string.Empty;

                        HelperTickets hticket = new HelperTickets();
                        hticket.IdTicket = ticket.Id;
                        hticket.IdUsuario = ticket.IdUsuarioLevanto;
                        hticket.IdGrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Id;
                        hticket.FechaHora = ticket.FechaHoraAlta;
                        hticket.NumeroTicket = ticket.Id;
                        hticket.NombreUsuario = ticket.UsuarioLevanto.NombreCompleto;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.GrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Descripcion;
                        hticket.EstatusTicket = ticket.EstatusTicket;
                        hticket.EstatusAsignacion = ticket.EstatusAsignacion;
                        hticket.IdUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id : 0;
                        hticket.UsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto : "";
                        hticket.NivelUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Aggregate(nivelAsignado, (current, usuarioAsignado) => current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion) : "";
                        hticket.EsPropietario = ticket.IdEstatusAsignacion == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar && supervisor ? true : idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado;
                        hticket.CambiaEstatus = hticket.IdUsuarioAsignado == idUsuario;
                        hticket.Asigna = grupoConSupervisor
                            ? (ticket.IdEstatusAsignacion == (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar && supervisor ? true
                            : idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado && ticket.IdEstatusTicket < (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto)
                            : lstEstatusPermitidos.Contains((int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar);

                        hticket.Total = totalRegistros;
                        hticket.IdImpacto = ticket.IdImpacto;
                        hticket.Impacto = ticket.Impacto.Descripcion;
                        result.Add(hticket);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public HelperDetalleTicket ObtenerDetalleTicket(int idTicket)
        {
            HelperDetalleTicket result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket);
                if (ticket != null)
                {
                    db.LoadProperty(ticket, "EstatusTicket");
                    db.LoadProperty(ticket, "EstatusAsignacion");
                    db.LoadProperty(ticket, "TicketEstatus");
                    foreach (TicketEstatus tEstatus in ticket.TicketEstatus)
                    {
                        db.LoadProperty(tEstatus, "EstatusTicket");
                        db.LoadProperty(tEstatus, "Usuario");
                    }
                    db.LoadProperty(ticket, "TicketAsignacion");
                    foreach (TicketAsignacion tAsignacion in ticket.TicketAsignacion)
                    {
                        db.LoadProperty(tAsignacion, "EstatusAsignacion");
                        db.LoadProperty(tAsignacion, "UsuarioAsignado");
                        db.LoadProperty(tAsignacion, "UsuarioAsigno");
                    }
                    result = new HelperDetalleTicket
                    {
                        IdTicket = ticket.Id,
                        IdEstatusTicket = ticket.IdEstatusTicket,
                        IdEstatusAsignacion = ticket.IdEstatusAsignacion,
                        EstatusActual = ticket.EstatusTicket.Descripcion,
                        AsignacionActual = ticket.EstatusAsignacion.Descripcion,
                        FechaCreacion = ticket.FechaHoraAlta,
                        EstatusDetalle = new List<HelperEstatusDetalle>(),
                        AsignacionesDetalle = new List<HelperAsignacionesDetalle>()
                    };
                    foreach (HelperEstatusDetalle detalle in ticket.TicketEstatus.Select(movEstatus => new HelperEstatusDetalle { Descripcion = movEstatus.EstatusTicket.Descripcion, UsuarioMovimiento = movEstatus.Usuario.NombreCompleto, FechaMovimiento = movEstatus.FechaMovimiento, Comentarios = movEstatus.Comentarios }))
                    {
                        result.EstatusDetalle.Add(detalle);
                    }
                    foreach (HelperAsignacionesDetalle detalle in ticket.TicketAsignacion.Select(movAsignacion => new HelperAsignacionesDetalle { Descripcion = movAsignacion.EstatusAsignacion.Descripcion, UsuarioAsignado = movAsignacion.UsuarioAsignado != null ? movAsignacion.UsuarioAsignado.NombreCompleto : "SIN ASGNACIÓN", UsuarioAsigno = movAsignacion.UsuarioAsigno != null ? movAsignacion.UsuarioAsigno.NombreCompleto : "NO APLICA", FechaMovimiento = movAsignacion.FechaAsignacion }))
                    {
                        result.AsignacionesDetalle.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public HelperDetalleTicket ObtenerDetalleTicketNoRegistrado(int idTicket, string cveRegistro)
        {
            HelperDetalleTicket result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket && t.Random && t.ClaveRegistro == cveRegistro);
                if (ticket != null)
                {
                    db.LoadProperty(ticket, "EstatusTicket");
                    db.LoadProperty(ticket, "EstatusAsignacion");
                    db.LoadProperty(ticket, "TicketEstatus");
                    foreach (TicketEstatus tEstatus in ticket.TicketEstatus)
                    {
                        db.LoadProperty(tEstatus, "EstatusTicket");
                        db.LoadProperty(tEstatus, "Usuario");
                    }
                    db.LoadProperty(ticket, "TicketAsignacion");
                    foreach (TicketAsignacion tAsignacion in ticket.TicketAsignacion)
                    {
                        db.LoadProperty(tAsignacion, "EstatusAsignacion");
                        db.LoadProperty(tAsignacion, "UsuarioAsignado");
                        db.LoadProperty(tAsignacion, "UsuarioAsigno");
                    }
                    result = new HelperDetalleTicket
                    {
                        IdTicket = ticket.Id,
                        IdEstatusTicket = ticket.IdEstatusTicket,
                        IdEstatusAsignacion = ticket.IdEstatusAsignacion,
                        EstatusActual = ticket.EstatusTicket.Descripcion,
                        AsignacionActual = ticket.EstatusAsignacion.Descripcion,
                        FechaCreacion = ticket.FechaHoraAlta,
                        EstatusDetalle = new List<HelperEstatusDetalle>(),
                        AsignacionesDetalle = new List<HelperAsignacionesDetalle>()
                    };
                    foreach (HelperEstatusDetalle detalle in ticket.TicketEstatus.Select(movEstatus => new HelperEstatusDetalle { Descripcion = movEstatus.EstatusTicket.Descripcion, UsuarioMovimiento = movEstatus.Usuario.NombreCompleto, FechaMovimiento = movEstatus.FechaMovimiento, Comentarios = movEstatus.Comentarios }))
                    {
                        result.EstatusDetalle.Add(detalle);
                    }
                    foreach (HelperAsignacionesDetalle detalle in ticket.TicketAsignacion.Select(movAsignacion => new HelperAsignacionesDetalle { Descripcion = movAsignacion.EstatusAsignacion.Descripcion, UsuarioAsignado = movAsignacion.UsuarioAsignado != null ? movAsignacion.UsuarioAsignado.NombreCompleto : "SIN ASGNACIÓN", UsuarioAsigno = movAsignacion.UsuarioAsigno != null ? movAsignacion.UsuarioAsigno.NombreCompleto : "NO APLICA", FechaMovimiento = movAsignacion.FechaAsignacion }))
                    {
                        result.AsignacionesDetalle.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public PreTicket GeneraPreticket(int idArbol, int idUsuarioSolicita, int idUsuarioLevanto, string observaciones)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            PreTicket result;
            try
            {
                result = new PreTicket
                {
                    IdArbol = idArbol,
                    IdUsuarioSolicito = idUsuarioSolicita,
                    IdUsuarioAtendio = idUsuarioLevanto,
                    FechaHora =
                        DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",
                            CultureInfo.InvariantCulture),
                    ClaveRegistro = GeneraCampoRandom(),
                    Observaciones = observaciones.Trim().ToUpper()
                };
                db.PreTicket.AddObject(result);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
    }
}
