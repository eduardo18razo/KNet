using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
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

        public void CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Usuario usuario = new BusinessUsuarios().ObtenerUsuario(idUsuario);
                ArbolAcceso arbol = new BusinessArbolAcceso().ObtenerArbolAcceso(idArbol);
                Mascara mascara =
                    new BusinessMascaras().ObtenerMascaraCaptura(arbol.InventarioArbolAcceso.First().IdMascara ?? 0);
                Encuesta encuesta =
                    new BusinessEncuesta().ObtenerEncuesta(arbol.InventarioArbolAcceso.First().IdEncuesta ?? 0);
                Sla sla = new BusinessSla().ObtenerSla(arbol.InventarioArbolAcceso.First().IdSla ?? 0);
                Ticket ticket = new Ticket
                {
                    IdTipoUsuario = usuario.IdTipoUsuario,
                    IdTipoArbolAcceso = arbol.IdTipoArbolAcceso,
                    IdArbolAcceso = arbol.Id,
                    IdUsuario = usuario.Id,
                    IdOrganizacion = usuario.IdOrganizacion,
                    IdUbicacion = usuario.IdUbicacion,
                    IdMascara = mascara.Id,
                    IdEncuesta = encuesta.Id,
                    RespuestaEncuesta = new List<RespuestaEncuesta>(),
                    IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto,
                    FechaHora =
                        DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",
                            CultureInfo.InvariantCulture),
                    IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar
                };
                ticket.RespuestaEncuesta.AddRange(
                    encuesta.EncuestaPregunta.Select(
                        pregunta => new RespuestaEncuesta { IdEncuesta = encuesta.Id, IdPregunta = pregunta.Id }));

                ticket.SlaEstimadoTicket = new SlaEstimadoTicket
                {
                    FechaInicio = DateTime.Now.Date,
                    FechaFin = DateTime.Now.Date,
                    TiempoHoraProceso = sla.TiempoHoraProceso,
                    Terminado = false,
                    SlaEstimadoTicketDetalle = new List<SlaEstimadoTicketDetalle>()
                };
                ticket.SlaEstimadoTicket.SlaEstimadoTicketDetalle.AddRange(
                    sla.SlaDetalle.Select(
                        detalle =>
                            new SlaEstimadoTicketDetalle
                            {
                                IdSubRol = detalle.IdSubRol,
                                TiempoProceso = detalle.TiempoProceso
                            }));
                ticket.TicketGrupoUsuario = new List<TicketGrupoUsuario>();
                foreach (
                    GrupoUsuarioInventarioArbol grupoArbol in
                        arbol.InventarioArbolAcceso.First()
                            .GrupoUsuarioInventarioArbol.Where(
                                w =>
                                    w.GrupoUsuario.IdTipoGrupo ==
                                    (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención)
                            .ToList())
                {
                    TicketGrupoUsuario grupo = new TicketGrupoUsuario { IdGrupoUsuario = grupoArbol.IdGrupoUsuario };
                    if (grupoArbol.IdSubGrupoUsuario != null)
                        grupo.IdSubGrupoUsuario = (int)grupoArbol.IdSubGrupoUsuario;
                    ticket.TicketGrupoUsuario.Add(grupo);
                }
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
                db.ExecuteStoreCommand(store);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
        }

        public void CambiarEstatus(int idTicket, int idEstatus, int idUsuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Ticket ticket = db.Ticket.SingleOrDefault(t => t.Id == idTicket);
                if (ticket != null)
                {
                    ticket.IdEstatusTicket = idEstatus;
                    ticket.TicketEstatus = new List<TicketEstatus>{new TicketEstatus
                    {
                        FechaMovimiento =  DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff",
                            CultureInfo.InvariantCulture),
                            IdEstatus = idEstatus,
                            IdUsuarioMovimiento = idUsuario
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

        public void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna)
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

        public List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperTickets> result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                List<Ticket> lstTickets = new List<Ticket>();

                foreach (int grupo in db.UsuarioGrupo.Where(ug => ug.IdUsuario == idUsuario && ug.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).Select(s => s.IdGrupoUsuario).Distinct())
                {
                    lstTickets.AddRange(db.Ticket.Join(db.TicketGrupoUsuario, t => t.Id, tg => tg.IdTicket, (t, tg) => new { t, tg }).Where(@t1 => @t1.tg.IdGrupoUsuario == grupo).Select(@t1 => @t1.t).Distinct().ToList());
                }
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
                        //{
                        hticket.IdTicket = ticket.Id;
                        hticket.IdUsuario = ticket.IdUsuario;
                        hticket.IdGrupoAsignado =
                            ticket.ArbolAcceso.InventarioArbolAcceso.First()
                                .GrupoUsuarioInventarioArbol.Where(
                                    s =>
                                        s.GrupoUsuario.IdTipoGrupo ==
                                        (int) BusinessVariables.EnumTiposGrupos.ResponsableDeAtención)
                                .Distinct()
                                .First()
                                .GrupoUsuario.Id;
                        hticket.FechaHora = (DateTime) ticket.FechaHora;
                        hticket.NumeroTicket = ticket.Id;
                        hticket.NombreUsuario = ticket.Usuario.NombreCompleto;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.GrupoAsignado =
                            ticket.ArbolAcceso.InventarioArbolAcceso.First()
                                .GrupoUsuarioInventarioArbol.Where(
                                    s =>
                                        s.GrupoUsuario.IdTipoGrupo ==
                                        (int) BusinessVariables.EnumTiposGrupos.ResponsableDeAtención)
                                .Distinct()
                                .First()
                                .GrupoUsuario.Descripcion;
                        hticket.EstatusTicket = ticket.EstatusTicket;
                        hticket.EstatusAsignacion = ticket.EstatusAsignacion;
                        var z = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado !=
                                                    null
                            ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id
                            : 0;
                        hticket.IdUsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado !=
                                                    null
                            ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.Id
                            : 0;
                        var y = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado !=
                                                  null
                            ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto
                            : "";
                        hticket.UsuarioAsignado = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado !=
                                                  null
                            ? ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado.NombreCompleto
                            : "";
                        
                        var test = ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null
                            ? ticket.TicketAsignacion.OrderBy(o => o.Id)
                                .Last()
                                .UsuarioAsignado.UsuarioGrupo.Where(w=>w.SubGrupoUsuario != null).Aggregate(nivelAsignado, (current, usuarioAsignado) => current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion)
                            : "";
                        hticket.NivelUsuarioAsignado =
                            ticket.TicketAsignacion.OrderBy(o => o.Id).Last().UsuarioAsignado != null
                                ? ticket.TicketAsignacion.OrderBy(o => o.Id)
                                    .Last()
                                    .UsuarioAsignado.UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Aggregate(nivelAsignado,
                                        (current, usuarioAsignado) =>
                                            current + usuarioAsignado.SubGrupoUsuario.SubRol.Descripcion)
                                : "";
                        hticket.EsPropietario = idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado;
                        hticket.Total = totalRegistros;
                        //};
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
                        FechaCreacion = ticket.FechaHora,
                        EstatusDetalle = new List<HelperEstatusDetalle>(),
                        AsignacionesDetalle = new List<HelperAsignacionesDetalle>()
                    };
                    foreach (HelperEstatusDetalle detalle in ticket.TicketEstatus.Select(movEstatus => new HelperEstatusDetalle { Descripcion = movEstatus.EstatusTicket.Descripcion, UsuarioMovimiento = movEstatus.Usuario.NombreCompleto, FechaMovimiento = movEstatus.FechaMovimiento }))
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
    }
}
