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
                Mascara mascara = new BusinessMascaras().ObtenerMascaraCaptura(arbol.InventarioArbolAcceso.First().IdMascara ?? 0);
                Encuesta encuesta = new BusinessEncuesta().ObtenerEncuesta(arbol.InventarioArbolAcceso.First().IdEncuesta ?? 0);
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
                    FechaHora = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar
                };
                ticket.RespuestaEncuesta.AddRange(encuesta.EncuestaPregunta.Select(pregunta => new RespuestaEncuesta { IdEncuesta = encuesta.Id, IdPregunta = pregunta.Id }));

                ticket.SlaEstimadoTicket = new SlaEstimadoTicket
                {
                    FechaInicio = DateTime.Now.Date,
                    FechaFin = DateTime.Now.Date,
                    TiempoHoraProceso = sla.TiempoHoraProceso,
                    Terminado = false,
                    SlaEstimadoTicketDetalle = new List<SlaEstimadoTicketDetalle>()
                };
                ticket.SlaEstimadoTicket.SlaEstimadoTicketDetalle.AddRange(sla.SlaDetalle.Select(detalle => new SlaEstimadoTicketDetalle { IdSubRol = detalle.IdSubRol, TiempoProceso = detalle.TiempoProceso }));
                ticket.TicketGrupoUsuario = new List<TicketGrupoUsuario>();
                foreach (GrupoUsuarioInventarioArbol grupoArbol in arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Where(w => w.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).ToList())
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
                        FechaMovimiento = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    }
                };
                ticket.TicketAsignacion = new List<TicketAsignacion>
                {
                    new TicketAsignacion
                    {
                        IdEstatusAsignacion = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar,
                        FechaAsignacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    }
                };

                db.Ticket.AddObject(ticket);
                db.SaveChanges();
                string store = string.Format("{0} '{1}',", mascara.ComandoInsertar, ticket.Id);
                store = lstCaptura.Aggregate(store, (current, captura) => current + string.Format("'{0}',", captura.Valor));
                store = store.Trim().TrimEnd(',');
                db.ExecuteStoreCommand(store);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                foreach (UsuarioGrupo grupo in db.UsuarioGrupo.Where(ug => ug.IdUsuario == idUsuario && ug.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención))
                {
                    lstTickets.AddRange(db.Ticket.Join(db.TicketGrupoUsuario, t => t.Id, tg => tg.IdTicket, (t, tg) => new { t, tg }).Where(@t1 => @t1.tg.IdGrupoUsuario == grupo.IdGrupoUsuario).Select(@t1 => @t1.t).ToList());
                }
                
                //List<Ticket> lstTickets = db.Ticket.Where(w => w.TicketGrupoUsuario.SelectMany(wg=> wg.IdGrupoUsuario == 1)).ToList();
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
                        db.LoadProperty(ticket, "ArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        HelperTickets hticket = new HelperTickets
                        {
                            IdTicket = ticket.Id,
                            IdUsuario = ticket.IdUsuario,
                            FechaHora = (DateTime) ticket.FechaHora,
                            NumeroTicket = ticket.Id,
                            NombreUsuario = ticket.Usuario.NombreCompleto,
                            Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso),
                            GrupoAsignado = ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Single(s => s.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).GrupoUsuario.Descripcion,
                            EstatusTicket = ticket.EstatusTicket,
                            EstatusAsignacion = ticket.EstatusAsignacion,
                            EsPropietario = idUsuario == ticket.TicketAsignacion.Last().IdUsuarioAsignado,
                            Total = totalRegistros
                        };
                        result.Add(hticket);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
