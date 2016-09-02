using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Tickets;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Core.Demonio;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessConsultas : IDisposable
    {
        private readonly bool _proxy;

        public void Dispose()
        {

        }

        public BusinessConsultas(bool proxy = false)
        {
            _proxy = proxy;
        }

        public List<HelperTickets> ConsultarTickets(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus, bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperTickets> result = null;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;

                var fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                var fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          where grupos.Contains(tgu.IdGrupoUsuario) && organizaciones.Contains(or.Id)
                                && ubicaciones.Contains(ub.Id) && tipoArbol.Contains(t.IdTipoArbolAcceso) &&
                                tipificacion.Contains(t.IdArbolAcceso) && prioridad.Contains(t.IdImpacto)
                                && estatus.Contains(t.IdEstatusTicket)
                                && t.DentroSla == sla
                                && t.FechaHoraAlta >= fechaInicio
                                && t.FechaHoraAlta <= fechaFin
                          select t;
                List<Ticket> lstTickets = qry.Distinct().ToList();

                int totalRegistros = lstTickets.Count;
                //TODO: Actualizar propiedades faltantes de asignacion
                if (totalRegistros > 0)
                {
                    result = new List<HelperTickets>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "Usuario");
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
                        hticket.IdUsuario = ticket.IdUsuario;
                        hticket.IdGrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Id;
                        hticket.FechaHora = ticket.FechaHoraAlta;
                        hticket.NumeroTicket = ticket.Id;
                        hticket.NombreUsuario = ticket.Usuario.NombreCompleto;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.GrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Descripcion;
                        hticket.EstatusTicket = ticket.EstatusTicket;
                        hticket.EstatusAsignacion = ticket.EstatusAsignacion;
                        hticket.IdUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id : 0;
                        hticket.UsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto : "";
                        hticket.NivelUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Aggregate(nivelAsignado, (current, usuarioAsignado) => current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion) : "";
                        hticket.CambiaEstatus = hticket.IdUsuarioAsignado == idUsuario;
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

        public List<HelperHits> ConsultarHits(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipificacion, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperHits> result = null;
            try
            {
                var fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                var fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var qry = from t in db.HitConsulta
                          join tgu in db.HitGrupoUsuario on t.Id equals tgu.IdHit
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          where grupos.Contains(tgu.IdGrupoUsuario) && organizaciones.Contains(or.Id)
                                && ubicaciones.Contains(ub.Id) && t.IdTipoArbolAcceso == (int)BusinessVariables.EnumTipoArbol.Consultas &&
                                tipificacion.Contains(t.IdArbolAcceso)
                                && t.FechaHoraAlta >= fechaInicio
                                && t.FechaHoraAlta <= fechaFin
                          select t;
                List<HitConsulta> lstHits = qry.Distinct().ToList();

                int totalRegistros = lstHits.Count;
                //TODO: Actualizar propiedades faltantes de asignacion
                if (totalRegistros > 0)
                {
                    result = new List<HelperHits>();
                    foreach (HitConsulta hit in lstHits.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(hit, "Usuario");
                        db.LoadProperty(hit, "Organizacion");
                        db.LoadProperty(hit, "Ubicacion");
                        db.LoadProperty(hit, "HitGrupoUsuario");

                        db.LoadProperty(hit, "TipoArbolAcceso");
                        db.LoadProperty(hit, "ArbolAcceso");
                        db.LoadProperty(hit, "ArbolAcceso");

                        HelperHits hHit = new HelperHits
                        {
                            IdHit = hit.Id,
                            IdUsuario = hit.IdUsuario,
                            FechaHora = hit.FechaHoraAlta,
                            NumeroHit = hit.Id,
                            NombreUsuario = hit.Usuario.NombreCompleto,
                            Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(hit.IdArbolAcceso),
                            Total = totalRegistros
                        };
                        result.Add(hHit);
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

        public List<HelperTickets> ConsultarEncuestas(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus, bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperTickets> result = null;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                var fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                var qry = from t in db.Ticket
                          join re in db.RespuestaEncuesta on t.Id equals re.IdTicket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          where grupos.Contains(tgu.IdGrupoUsuario) && organizaciones.Contains(or.Id)
                                && ubicaciones.Contains(ub.Id) && tipoArbol.Contains(t.IdTipoArbolAcceso) &&
                                tipificacion.Contains(t.IdArbolAcceso) && prioridad.Contains(t.IdImpacto)
                                && estatus.Contains(t.IdEstatusTicket)
                                && t.DentroSla == sla
                                && t.FechaHoraAlta >= fechaInicio
                                && t.FechaHoraAlta <= fechaFin
                          select t;
                List<Ticket> lstTickets = qry.Distinct().ToList();

                int totalRegistros = lstTickets.Count;
                //TODO: Actualizar propiedades faltantes de asignacion
                if (totalRegistros > 0)
                {
                    result = new List<HelperTickets>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "Usuario");
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
                        hticket.IdUsuario = ticket.IdUsuario;
                        hticket.IdGrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Id;
                        hticket.FechaHora = ticket.FechaHoraAlta;
                        hticket.NumeroTicket = ticket.Id;
                        hticket.NombreUsuario = ticket.Usuario.NombreCompleto;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.GrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Distinct().First().GrupoUsuario.Descripcion;
                        hticket.EstatusTicket = ticket.EstatusTicket;
                        hticket.EstatusAsignacion = ticket.EstatusAsignacion;
                        hticket.IdUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id : 0;
                        hticket.UsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto : "";
                        hticket.NivelUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Aggregate(nivelAsignado, (current, usuarioAsignado) => current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion) : "";
                        hticket.CambiaEstatus = hticket.IdUsuarioAsignado == idUsuario;
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
    }
}
