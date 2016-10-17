using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Tickets;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Core.Demonio;
using KinniNet.Core.Sistema;
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

        #region Consultas
        public List<HelperReportesTicket> ConsultarTickets(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus, List<bool?> sla, List<bool?> vip, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperReportesTicket> result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          select t;

                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.IdTipoUsuario)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.IdOrganizacion)
                          select q;
                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.IdUbicacion)
                          select q;
                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.IdTipoArbolAcceso)
                          select q;

                if (tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.IdArbolAcceso)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.IdImpacto)
                          select q;

                if (estatus.Any())
                    qry = from q in qry
                          where estatus.Contains(q.IdEstatusTicket)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.DentroSla)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.UsuarioLevanto.Vip)
                          select q;

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.FechaHoraAlta >= fechaInicio
                                    && q.FechaHoraAlta <= fechaFin
                              select q;

                List<Ticket> lstTickets = qry.Distinct().ToList();

                int totalRegistros = lstTickets.Count;
                if (totalRegistros > 0)
                {
                    result = new List<HelperReportesTicket>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "TipoUsuario");
                        db.LoadProperty(ticket, "EstatusTicket");
                        db.LoadProperty(ticket, "EstatusAsignacion");
                        db.LoadProperty(ticket, "TicketEstatus");
                        db.LoadProperty(ticket, "TicketAsignacion");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket, "Organizacion");
                        db.LoadProperty(ticket, "Ubicacion");
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
                        db.LoadProperty(ticket, "TipoArbolAcceso");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket.Impacto, "Prioridad");
                        db.LoadProperty(ticket.Impacto, "Urgencia");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        db.LoadProperty(ticket, "TicketGrupoUsuario");
                        foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario)
                        {
                            db.LoadProperty(tgu, "GrupoUsuario");
                        }

                        HelperReportesTicket hticket = new HelperReportesTicket
                        {
                            IdTicket = ticket.Id,
                            TipoUsuario = ticket.TipoUsuario.Descripcion
                        };

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).IdGrupoUsuario))
                                hticket.GrupoEspecialConsulta = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).IdGrupoUsuario))
                                hticket.GrupoAtendedor = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).IdGrupoUsuario))
                                hticket.GrupoMantenimiento = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).IdGrupoUsuario))
                                hticket.GrupoOperacion = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).IdGrupoUsuario))
                                hticket.GrupoDesarrollo = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).GrupoUsuario.Descripcion;

                        hticket.IdOrganizacion = ticket.IdOrganizacion;
                        hticket.Organizacion = new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(ticket.IdOrganizacion, false);
                        hticket.IdNivelOrganizacion = ticket.Organizacion.IdNivelOrganizacion;

                        hticket.IdUbicacion = ticket.IdUbicacion;
                        hticket.Ubicacion = new BusinessUbicacion().ObtenerDescripcionUbicacionById(ticket.IdUbicacion, false);
                        hticket.IdNivelUbicacion = ticket.Ubicacion.IdNivelUbicacion;

                        hticket.IdTipificacion = ticket.IdArbolAcceso;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.IdServicioIncidente = ticket.IdTipoArbolAcceso;
                        hticket.ServicioIncidente = ticket.TipoArbolAcceso.Descripcion;
                        hticket.Prioridad = ticket.Impacto.Prioridad.Descripcion;
                        hticket.Urgencia = ticket.Impacto.Urgencia.Descripcion;
                        hticket.Impacto = ticket.Impacto.Descripcion;
                        hticket.IdEstatus = ticket.IdEstatusTicket;
                        hticket.Estatus = ticket.EstatusTicket.Descripcion;
                        hticket.DentroSla = ticket.DentroSla;
                        hticket.Sla = ticket.DentroSla ? "DENTRO" : "FUERA";
                        hticket.FechaHora = ticket.FechaHoraAlta.ToString("dd/MM/yyyy");
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

        public List<HelperHits> ConsultarHits(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipificacion, List<bool?> vip, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperHits> result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }
                var qry = from h in db.HitConsulta
                          join tgu in db.HitGrupoUsuario on h.Id equals tgu.IdHit
                          join or in db.Organizacion on h.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on h.IdUbicacion equals ub.Id
                          select h;
                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.HitGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.Usuario.IdTipoUsuario)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.IdOrganizacion)
                          select q;
                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.IdUbicacion)
                          select q;

                if (tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.IdArbolAcceso)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.Usuario.Vip)
                          select q;

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.FechaHoraAlta >= fechaInicio
                                    && q.FechaHoraAlta <= fechaFin
                              select q;

                List<HitConsulta> lstHits = qry.Distinct().ToList();

                int totalRegistros = lstHits.Count;
                if (totalRegistros > 0)
                {
                    result = new List<HelperHits>();
                    foreach (HitConsulta hit in lstHits.Skip(pageIndex * pageSize).Take(pageSize).OrderBy(o => o.IdArbolAcceso))
                    {
                        db.LoadProperty(hit, "Usuario");
                        db.LoadProperty(hit.Usuario, "TipoUsuario");
                        db.LoadProperty(hit, "Organizacion");
                        db.LoadProperty(hit, "Ubicacion");
                        db.LoadProperty(hit, "HitGrupoUsuario");
                        db.LoadProperty(hit, "TipoArbolAcceso");
                        db.LoadProperty(hit, "ArbolAcceso");

                        HelperHits hHit = new HelperHits
                        {
                            IdHit = hit.Id,
                            IdTipoArbolAcceso = hit.IdTipoArbolAcceso,
                            IdTipificacion = hit.IdArbolAcceso,
                            IdUsuario = hit.IdUsuario,
                            IdUbicacion = hit.IdUbicacion,
                            IdOrganizacion = hit.IdOrganizacion,
                            TipoServicio = hit.TipoArbolAcceso.Descripcion,
                            Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(hit.IdArbolAcceso),
                            NombreUsuario = hit.Usuario.NombreCompleto,
                            Ubicacion = new BusinessUbicacion().ObtenerDescripcionUbicacionById(hit.IdUbicacion, false),
                            Organizacion = new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(hit.IdOrganizacion, false),
                            FechaHora = hit.FechaHoraAlta.ToString("dd/MM/yyyy"),
                            Total = lstHits.Count(c => c.IdArbolAcceso == hit.IdArbolAcceso && c.IdUsuario == hit.IdUsuario)
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

        public List<HelperReportesTicket> ConsultarEncuestas(int idUsuario, List<int> grupos, List<int> tipoArbol, List<int> responsables, List<int?> encuestas, List<int> atendedores,
             Dictionary<string, DateTime> fechas,
            List<int> tiposUsuario,
             List<int> prioridad,
            List<bool?> sla,
            List<int> ubicaciones,
            List<int> organizaciones, List<bool?> vip, int pageIndex, int pageSize)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperReportesTicket> result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          where t.EncuestaRespondida
                          select new { t, tgu, or, ub, ug };


                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.t.IdTipoArbolAcceso)
                          select q;
                if (responsables.Any())
                    qry = responsables.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (encuestas.Any())
                    qry = from q in qry
                          where encuestas.Contains(q.t.IdEncuesta)
                          select q;

                if (atendedores.Any())
                    qry = atendedores.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.t.IdTipoUsuario)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.t.IdImpacto)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.t.DentroSla)
                          select q;

                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.t.IdUbicacion)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.t.IdOrganizacion)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.t.UsuarioLevanto.Vip)
                          select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                List<Ticket> lstTickets = qry.Select(s => s.t).Distinct().ToList();

                int totalRegistros = lstTickets.Count;
                if (totalRegistros > 0)
                {
                    result = new List<HelperReportesTicket>();
                    foreach (Ticket ticket in lstTickets.Skip(pageIndex * pageSize).Take(pageSize))
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "TipoUsuario");
                        db.LoadProperty(ticket, "EstatusTicket");
                        db.LoadProperty(ticket, "EstatusAsignacion");
                        db.LoadProperty(ticket, "TicketEstatus");
                        db.LoadProperty(ticket, "TicketAsignacion");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket, "Organizacion");
                        db.LoadProperty(ticket, "Ubicacion");
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
                        db.LoadProperty(ticket, "TipoArbolAcceso");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket.Impacto, "Prioridad");
                        db.LoadProperty(ticket.Impacto, "Urgencia");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        db.LoadProperty(ticket, "TicketGrupoUsuario");
                        foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario)
                        {
                            db.LoadProperty(tgu, "GrupoUsuario");
                        }

                        HelperReportesTicket hticket = new HelperReportesTicket
                        {
                            IdTicket = ticket.Id,
                            TipoUsuario = ticket.TipoUsuario.Descripcion
                        };

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).IdGrupoUsuario))
                                hticket.GrupoEspecialConsulta = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).IdGrupoUsuario))
                                hticket.GrupoAtendedor = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).IdGrupoUsuario))
                                hticket.GrupoMantenimiento = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).IdGrupoUsuario))
                                hticket.GrupoOperacion = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo))
                            if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).IdGrupoUsuario))
                                hticket.GrupoDesarrollo = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).GrupoUsuario.Descripcion;

                        hticket.IdOrganizacion = ticket.IdOrganizacion;
                        hticket.Organizacion = new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(ticket.IdOrganizacion, false);
                        hticket.IdNivelOrganizacion = ticket.Organizacion.IdNivelOrganizacion;

                        hticket.IdUbicacion = ticket.IdUbicacion;
                        hticket.Ubicacion = new BusinessUbicacion().ObtenerDescripcionUbicacionById(ticket.IdUbicacion, false);
                        hticket.IdNivelUbicacion = ticket.Ubicacion.IdNivelUbicacion;

                        hticket.IdTipificacion = ticket.IdArbolAcceso;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.IdServicioIncidente = ticket.IdTipoArbolAcceso;
                        hticket.ServicioIncidente = ticket.TipoArbolAcceso.Descripcion;
                        hticket.Prioridad = ticket.Impacto.Prioridad.Descripcion;
                        hticket.Urgencia = ticket.Impacto.Urgencia.Descripcion;
                        hticket.Impacto = ticket.Impacto.Descripcion;
                        hticket.IdEstatus = ticket.IdEstatusTicket;
                        hticket.Estatus = ticket.EstatusTicket.Descripcion;
                        hticket.DentroSla = ticket.DentroSla;
                        hticket.Sla = ticket.DentroSla ? "DENTRO" : "FUERA";
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

        public List<HelperReportesTicket> ConsultaEncuestaPregunta(int idUsuario, int idEncuesta, Dictionary<string, DateTime> fechas, int tipoFecha, int tipoEncuesta, int idPregunta)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperReportesTicket> result = null;
            DateTime fechaInicio = new DateTime();
            int conteo = 1;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }
                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join ep in db.EncuestaPregunta on new { idenctick = (int)t.IdEncuesta, ids = e.Id } equals new { idenctick = ep.IdEncuesta, ids = ep.IdEncuesta }
                          join re in db.RespuestaEncuesta on
                          new { idTickTick = t.Id, idEncTick = (int)t.IdEncuesta, idEncPadre = e.Id, idPreg = ep.Id } equals
                          new { idTickTick = re.IdTicket, idEncTick = re.IdEncuesta, idEncPadre = re.IdEncuesta, idPreg = re.IdPregunta }
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          where t.EncuestaRespondida && t.IdEncuesta == idEncuesta
                          select new { t, tgu, ug, e, re, ep };

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                var lstTickets = qry.Select(s => s.t).Distinct().ToList();
                int totalRegistros = lstTickets.Count;
                if (totalRegistros > 0)
                {
                    result = new List<HelperReportesTicket>();
                    foreach (Ticket ticket in lstTickets)
                    {
                        db.LoadProperty(ticket, "UsuarioLevanto");
                        db.LoadProperty(ticket, "TipoUsuario");
                        db.LoadProperty(ticket, "EstatusTicket");
                        db.LoadProperty(ticket, "EstatusAsignacion");
                        db.LoadProperty(ticket, "TicketEstatus");
                        db.LoadProperty(ticket, "TicketAsignacion");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket, "Organizacion");
                        db.LoadProperty(ticket, "Ubicacion");
                        db.LoadProperty(ticket, "RespuestaEncuesta");
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
                        db.LoadProperty(ticket, "TipoArbolAcceso");
                        db.LoadProperty(ticket, "Impacto");
                        db.LoadProperty(ticket.Impacto, "Prioridad");
                        db.LoadProperty(ticket.Impacto, "Urgencia");
                        db.LoadProperty(ticket.ArbolAcceso, "InventarioArbolAcceso");
                        db.LoadProperty(ticket.ArbolAcceso.InventarioArbolAcceso.First(), "GrupoUsuarioInventarioArbol");
                        foreach (GrupoUsuarioInventarioArbol grupoinv in ticket.ArbolAcceso.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol)
                        {
                            db.LoadProperty(grupoinv, "GrupoUsuario");
                        }
                        db.LoadProperty(ticket, "TicketGrupoUsuario");
                        foreach (TicketGrupoUsuario tgu in ticket.TicketGrupoUsuario)
                        {
                            db.LoadProperty(tgu, "GrupoUsuario");
                        }

                        HelperReportesTicket hticket = new HelperReportesTicket
                        {
                            IdTicket = ticket.Id,
                            TipoUsuario = ticket.TipoUsuario.Descripcion
                        };

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta))
                            //    if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).IdGrupoUsuario))
                            hticket.GrupoEspecialConsulta = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención))
                            //    if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).IdGrupoUsuario))
                            hticket.GrupoAtendedor = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada))
                            //    if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).IdGrupoUsuario))
                            hticket.GrupoMantenimiento = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación))
                            //    if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).IdGrupoUsuario))
                            hticket.GrupoOperacion = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación).GrupoUsuario.Descripcion;

                        if (ticket.TicketGrupoUsuario.Any(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo))
                            //    if (grupos.Contains(ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).IdGrupoUsuario))
                            hticket.GrupoDesarrollo = ticket.TicketGrupoUsuario.First(f => f.GrupoUsuario.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo).GrupoUsuario.Descripcion;

                        hticket.IdOrganizacion = ticket.IdOrganizacion;
                        hticket.Organizacion = new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(ticket.IdOrganizacion, false);
                        hticket.IdNivelOrganizacion = ticket.Organizacion.IdNivelOrganizacion;

                        hticket.IdUbicacion = ticket.IdUbicacion;
                        hticket.Ubicacion = new BusinessUbicacion().ObtenerDescripcionUbicacionById(ticket.IdUbicacion, false);
                        hticket.IdNivelUbicacion = ticket.Ubicacion.IdNivelUbicacion;

                        hticket.IdTipificacion = ticket.IdArbolAcceso;
                        hticket.Tipificacion = new BusinessArbolAcceso().ObtenerTipificacion(ticket.IdArbolAcceso);
                        hticket.IdServicioIncidente = ticket.IdTipoArbolAcceso;
                        hticket.ServicioIncidente = ticket.TipoArbolAcceso.Descripcion;
                        hticket.Prioridad = ticket.Impacto.Prioridad.Descripcion;
                        hticket.Urgencia = ticket.Impacto.Urgencia.Descripcion;
                        hticket.Impacto = ticket.Impacto.Descripcion;
                        hticket.IdEstatus = ticket.IdEstatusTicket;
                        hticket.Estatus = ticket.EstatusTicket.Descripcion;
                        hticket.DentroSla = ticket.DentroSla;
                        hticket.Sla = ticket.DentroSla ? "DENTRO" : "FUERA";
                        //hticket.Respuesta = ticket.RespuestaEncuesta
                        hticket.FechaHora = ticket.FechaHoraAlta.ToString("dd/MM/yyyy");
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

        #endregion Consultas

        #region Graficas
        public DataTable GraficarConsultaTicket(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus, List<bool?> sla, List<bool?> vip, Dictionary<string, DateTime> fechas, List<int> filtroStackColumn, string stack, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            DataTable result = null;
            DateTime fechaInicio = new DateTime();
            int conteo = 1;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          select t;

                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.IdTipoUsuario)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.IdOrganizacion)
                          select q;
                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.IdUbicacion)
                          select q;
                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.IdTipoArbolAcceso)
                          select q;

                if (tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.IdArbolAcceso)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.IdImpacto)
                          select q;

                if (estatus.Any())
                    qry = from q in qry
                          where estatus.Contains(q.IdEstatusTicket)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.DentroSla)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.UsuarioLevanto.Vip)
                          select q;

                if (fechas != null)
                {
                    if (fechas.Count == 2)
                    {
                        qry = from q in qry
                              where q.FechaHoraAlta >= fechaInicio
                                    && q.FechaHoraAlta <= fechaFin
                              select q;
                    }
                }

                List<Ticket> lstTickets = qry.Distinct().ToList();

                if (lstTickets.Any())
                {
                    result = new DataTable("dt");
                    result.Columns.Add(new DataColumn("Id"));
                    result.Columns.Add(new DataColumn("Descripcion"));

                    List<string> lstFechas = lstTickets.OrderBy(o => o.FechaHoraAlta).Distinct().Select(s => s.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                    switch (tipoFecha)
                    {
                        case 1:
                            foreach (string fecha in lstFechas)
                            {
                                result.Columns.Add(fecha);
                            }
                            break;
                        case 2:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                conteo++;
                            }
                            break;
                        case 3:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                            }
                            break;
                        case 4:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                            }
                            break;
                    }

                    int row = 0;
                    switch (stack)
                    {
                        case "Ubicaciones":

                            foreach (int idUbicacion in lstTickets.Select(s => s.IdUbicacion).Distinct())
                            {
                                result.Rows.Add(idUbicacion, new BusinessUbicacion().ObtenerDescripcionUbicacionById(idUbicacion, false));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdUbicacion == idUbicacion);
                                            break;
                                    }

                                }
                                row++;
                            }
                            break;
                        case "Organizaciones":
                            foreach (int idOrganizacion in lstTickets.Select(s => s.IdOrganizacion).Distinct())
                            {
                                result.Rows.Add(idOrganizacion, new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(idOrganizacion, false));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdOrganizacion == idOrganizacion);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "Tipo Ticket":
                            foreach (int idTipoArbolAcceso in lstTickets.Select(s => s.IdTipoArbolAcceso).Distinct())
                            {
                                result.Rows.Add(idTipoArbolAcceso, new BusinessTipoArbolAcceso().ObtenerTiposArbolAcceso(false).Single(s => s.Id == idTipoArbolAcceso).Descripcion);
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "Tipificaciones":
                            foreach (int idArbol in lstTickets.Select(s => s.IdArbolAcceso).Distinct())
                            {
                                result.Rows.Add(idArbol, new BusinessArbolAcceso().ObtenerTipificacion(idArbol));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdArbolAcceso == idArbol);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "Estatus Ticket":
                            foreach (int idEstatusticket in lstTickets.Select(s => s.IdEstatusTicket).Distinct())
                            {
                                result.Rows.Add(idEstatusticket, new BusinessEstatus().ObtenerEstatusTicket(false).Single(s => s.Id == idEstatusticket).Descripcion);
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdEstatusTicket == idEstatusticket);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdEstatusTicket == idEstatusticket);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdEstatusTicket == idEstatusticket);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdEstatusTicket == idEstatusticket);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "SLA":
                            foreach (bool dentroSla in lstTickets.Select(s => s.DentroSla).Distinct())
                            {
                                result.Rows.Add(dentroSla ? 1 : 0, dentroSla ? "Dentro" : "Fuera");
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.DentroSla == dentroSla);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.DentroSla == dentroSla);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.DentroSla == dentroSla);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.DentroSla == dentroSla);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
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

        public string GraficarConsultaTicketGeografico(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus, List<bool?> sla, List<bool?> vip, Dictionary<string, DateTime> fechas, List<int> filtroStackColumn, string stack, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            string result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          join d in db.Domicilio on ub.IdCampus equals d.IdCampus
                          join col in db.Colonia on d.IdColonia equals col.Id
                          join m in db.Municipio on col.IdMunicipio equals m.Id
                          join e in db.Estado on m.IdEstado equals e.Id
                          select new { t, e };

                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.t.IdTipoUsuario)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.t.IdOrganizacion)
                          select q;
                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.t.IdUbicacion)
                          select q;
                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.t.IdTipoArbolAcceso)
                          select q;

                if (tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.t.IdArbolAcceso)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.t.IdImpacto)
                          select q;

                if (estatus.Any())
                    qry = from q in qry
                          where estatus.Contains(q.t.IdEstatusTicket)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.t.DentroSla)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.t.UsuarioLevanto.Vip)
                          select q;

                if (fechas != null)
                {
                    if (fechas.Count == 2)
                    {
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;
                    }
                }

                var lstTickets = (from q in qry.Distinct()
                                  group new { q.e.RegionCode } by new { q.e.RegionCode }
                                      into g
                                      select new { g.Key.RegionCode, Hits = g.Count() }).ToList();
                if (lstTickets.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Region");
                    dt.Columns.Add("Tickets");
                    foreach (var data in lstTickets)
                    {
                        dt.Rows.Add(data.RegionCode, data.Hits);
                    }
                    result = dt.Columns.Cast<DataColumn>().Aggregate("[\n[", (current, column) => current + string.Format("'{0}', ", column.ColumnName));
                    result = result.Trim().TrimEnd(',') + "], \n";
                    result = dt.Rows.Cast<DataRow>().Aggregate(result, (current, row) => current + string.Format("['{0}', {1}]\n", row[0], row[1]));
                    result += "]";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public DataTable GraficarConsultaHits(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipificacion, List<bool?> vip, Dictionary<string, DateTime> fechas, List<int> filtroStackColumn, string stack, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            DataTable result = null;
            DateTime fechaInicio = new DateTime();
            int conteo = 1;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from h in db.HitConsulta
                          join tgu in db.HitGrupoUsuario on h.Id equals tgu.IdHit
                          join or in db.Organizacion on h.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on h.IdUbicacion equals ub.Id
                          select h;

                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.HitGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.Usuario.IdTipoUsuario)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.IdOrganizacion)
                          select q;
                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.IdUbicacion)
                          select q;

                if (tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.IdArbolAcceso)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.Usuario.Vip)
                          select q;

                if (fechas != null)
                {
                    if (fechas.Count == 2)
                    {
                        qry = from q in qry
                              where q.FechaHoraAlta >= fechaInicio
                                    && q.FechaHoraAlta <= fechaFin
                              select q;
                    }
                }

                List<HitConsulta> lstTickets = qry.Distinct().ToList();

                if (lstTickets.Any())
                {
                    result = new DataTable("dt");
                    result.Columns.Add(new DataColumn("Id"));
                    result.Columns.Add(new DataColumn("Descripcion"));

                    List<string> lstFechas = lstTickets.OrderBy(o => o.FechaHoraAlta).Distinct().Select(s => s.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                    switch (tipoFecha)
                    {
                        case 1:
                            foreach (string fecha in lstFechas)
                            {
                                result.Columns.Add(fecha);
                            }
                            break;
                        case 2:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                conteo++;
                            }
                            break;
                        case 3:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                            }
                            break;
                        case 4:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                            }
                            break;
                    }

                    int row = 0;
                    switch (stack)
                    {
                        case "Ubicaciones":

                            foreach (int idUbicacion in lstTickets.Select(s => s.IdUbicacion).Distinct())
                            {
                                result.Rows.Add(idUbicacion, new BusinessUbicacion().ObtenerDescripcionUbicacionById(idUbicacion, false));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdUbicacion == idUbicacion);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdUbicacion == idUbicacion);
                                            break;
                                    }

                                }
                                row++;
                            }
                            break;
                        case "Organizaciones":
                            foreach (int idOrganizacion in lstTickets.Select(s => s.IdOrganizacion).Distinct())
                            {
                                result.Rows.Add(idOrganizacion, new BusinessOrganizacion().ObtenerDescripcionOrganizacionById(idOrganizacion, false));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdOrganizacion == idOrganizacion);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdOrganizacion == idOrganizacion);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "Tipo Ticket":
                            foreach (int idTipoArbolAcceso in lstTickets.Select(s => s.IdTipoArbolAcceso).Distinct())
                            {
                                result.Rows.Add(idTipoArbolAcceso, new BusinessTipoArbolAcceso().ObtenerTiposArbolAcceso(false).Single(s => s.Id == idTipoArbolAcceso).Descripcion);
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdTipoArbolAcceso == idTipoArbolAcceso);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
                        case "Tipificaciones":
                            foreach (int idArbol in lstTickets.Select(s => s.IdArbolAcceso).Distinct())
                            {
                                result.Rows.Add(idArbol, new BusinessArbolAcceso().ObtenerTipificacion(idArbol));
                                for (int i = 2; i < result.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 2:

                                            result.Rows[row][i] = lstTickets.Count(c =>
                                                DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                                && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 3:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdArbolAcceso == idArbol);
                                            break;
                                        case 4:
                                            result.Rows[row][i] = lstTickets.Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdArbolAcceso == idArbol);
                                            break;
                                    }
                                }
                                row++;
                            }
                            break;
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

        public string GraficarConsultaHitsGeografico(int idUsuario, List<int> grupos, List<int> tiposUsuario, List<int> organizaciones, List<int> ubicaciones, List<int> tipificacion, List<bool?> vip, Dictionary<string, DateTime> fechas, List<int> filtroStackColumn, string stack, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            string result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from h in db.HitConsulta
                          join tgu in db.HitGrupoUsuario on h.Id equals tgu.IdHit
                          join or in db.Organizacion on h.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on h.IdUbicacion equals ub.Id
                          join d in db.Domicilio on ub.IdCampus equals d.IdCampus
                          join col in db.Colonia on d.IdColonia equals col.Id
                          join m in db.Municipio on col.IdMunicipio equals m.Id
                          join e in db.Estado on m.IdEstado equals e.Id
                          select new { h, e };

                if (grupos != null && grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.h.HitGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tiposUsuario != null && tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.h.Usuario.IdTipoUsuario)
                          select q;

                if (organizaciones != null && organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.h.IdOrganizacion)
                          select q;
                if (ubicaciones != null && ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.h.IdUbicacion)
                          select q;

                if (tipificacion != null && tipificacion.Any())
                    qry = from q in qry
                          where tipificacion.Contains(q.h.IdArbolAcceso)
                          select q;

                if (vip != null && vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.h.Usuario.Vip)
                          select q;

                if (fechas != null && fechas != null)
                {
                    if (fechas.Count == 2)
                    {
                        qry = from q in qry
                              where q.h.FechaHoraAlta >= fechaInicio
                                    && q.h.FechaHoraAlta <= fechaFin
                              select q;
                    }
                }

                var lstTickets = (from q in qry.Distinct()
                                  group new { q.e.RegionCode } by new { q.e.RegionCode }
                                      into g
                                      select new { g.Key.RegionCode, Hits = g.Count() }).ToList();

                if (lstTickets.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Region");
                    dt.Columns.Add("Hits");
                    foreach (var data in lstTickets)
                    {
                        dt.Rows.Add(data.RegionCode, data.Hits);
                    }
                    result = dt.Columns.Cast<DataColumn>().Aggregate("[\n[", (current, column) => current + string.Format("'{0}', ", column.ColumnName));
                    result = result.Trim().TrimEnd(',') + "], \n";
                    result = dt.Rows.Cast<DataRow>().Aggregate(result, (current, row) => current + string.Format("['{0}', {1}]\n", row[0], row[1]));
                    result += "]";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public DataTable GraficarConsultaEncuesta(int idUsuario, List<int> grupos, List<int> tipoArbol, List<int> responsables, List<int?> encuestas, List<int> atendedores, Dictionary<string, DateTime> fechas, List<int> tiposUsuario, List<int> prioridad, List<bool?> sla, List<int> ubicaciones, List<int> organizaciones, List<bool?> vip, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            DataTable result = null;
            DateTime fechaInicio = new DateTime();
            int conteo = 1;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join er in db.RespuestaEncuesta on new { idtick = t.Id, padre = e.Id } equals new { idtick = er.IdTicket, padre = er.IdEncuesta }
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          where t.EncuestaRespondida
                          select new { t, tgu, or, ub, ug, e, er };


                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.t.IdTipoArbolAcceso)
                          select q;
                if (responsables.Any())
                    qry = responsables.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (encuestas.Any())
                    qry = from q in qry
                          where encuestas.Contains(q.t.IdEncuesta)
                          select q;

                if (atendedores.Any())
                    qry = atendedores.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.t.IdTipoUsuario)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.t.IdImpacto)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.t.DentroSla)
                          select q;

                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.t.IdUbicacion)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.t.IdOrganizacion)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.t.UsuarioLevanto.Vip)
                          select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                var lstTickets = qry.Distinct().ToList();

                if (lstTickets.Any())
                {
                    result = new DataTable("dt");
                    result.Columns.Add(new DataColumn("Descripcion"));
                    result.Columns.Add(new DataColumn("Total"));

                    List<string> lstFechas = lstTickets.OrderBy(o => o.t.FechaHoraAlta).Distinct().Select(s => s.t.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                    switch (tipoFecha)
                    {
                        case 1:
                            foreach (string fecha in lstFechas)
                            {
                                result.Columns.Add(fecha);
                            }
                            break;
                        case 2:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                conteo++;
                            }
                            break;
                        case 3:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                            }
                            break;
                        case 4:
                            foreach (string fecha in lstFechas)
                            {
                                if (!result.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                    result.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                            }
                            break;
                    }

                    int row = 0;
                    foreach (int? idEncuesta in lstTickets.Select(s => s.t.IdEncuesta).Distinct())
                    {
                        if (idEncuesta == null) continue;
                        result.Rows.Add(new BusinessEncuesta().ObtenerEncuestaById((int)idEncuesta).Descripcion);
                        var enumeracion = lstTickets.Where(w => w.t.IdEncuesta == idEncuesta).SelectMany(s => s.e.RespuestaEncuesta).Distinct().ToList().GroupBy(ge => new { ge.IdTicket, ge.IdEncuesta, ge.Ponderacion })
                                .Distinct().Select(ssum =>
                                        new
                                        {
                                            ssum.Key.IdTicket,
                                            ssum.Key.IdEncuesta,
                                            SumaEncuesta = ssum.Sum(sumas => sumas.Ponderacion)
                                        })
                                .Distinct().ToList();
                        var sumEncuesta = enumeracion.Select(s => new { s.IdTicket, TotalEncuesta = enumeracion.Where(w => w.IdTicket == s.IdTicket).Sum(sm => sm.SumaEncuesta) }).Distinct();
                        var total = sumEncuesta.Average(av => av.TotalEncuesta);

                        result.Rows[row][1] = total;
                        for (int i = 2; i < result.Columns.Count; i++)
                        {
                            switch (tipoFecha)
                            {
                                case 1:
                                    result.Rows[row][i] =
                                        lstTickets.Select(s => s.t).Distinct().Count(c => c.FechaHoraAlta.ToString("dd/MM/yyyy") == result.Columns[i].ColumnName && c.IdEncuesta == idEncuesta);
                                    break;
                                case 2:

                                    result.Rows[row][i] = lstTickets.Select(s => s.t).Distinct().Count(c =>
                                        DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                        && DateTime.Parse(c.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(result.Columns[i].ColumnName.Split(' ')[3]), int.Parse(result.Columns[i].ColumnName.Split(' ')[1]))
                                        && c.IdEncuesta == idEncuesta);
                                    break;
                                case 3:
                                    result.Rows[row][i] = lstTickets.Select(s => s.t).Distinct().Count(c => c.FechaHoraAlta.ToString("MM") == result.Columns[i].ColumnName.PadLeft(2, '0') && c.IdEncuesta == idEncuesta);
                                    break;
                                case 4:
                                    result.Rows[row][i] = lstTickets.Select(s => s.t).Distinct().Count(c => c.FechaHoraAlta.ToString("yyyy") == result.Columns[i].ColumnName.PadLeft(4, '0') && c.IdEncuesta == idEncuesta);
                                    break;
                            }

                        }
                        row++;
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

        public string GraficarConsultaEncuestaGeografica(int idUsuario, List<int> grupos, List<int> tipoArbol, List<int> responsables, List<int?> encuestas, List<int> atendedores, Dictionary<string, DateTime> fechas, List<int> tiposUsuario, List<int> prioridad, List<bool?> sla, List<int> ubicaciones, List<int> organizaciones, List<bool?> vip, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            string result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join re in db.RespuestaEncuesta on new { idTickTick = t.Id, idEncTick = (int)t.IdEncuesta, idEncPadre = e.Id } equals new { idTickTick = re.IdTicket, idEncTick = re.IdEncuesta, idEncPadre = re.IdEncuesta }
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join or in db.Organizacion on t.IdOrganizacion equals or.Id
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          join d in db.Domicilio on ub.IdCampus equals d.IdCampus
                          join col in db.Colonia on d.IdColonia equals col.Id
                          join m in db.Municipio on col.IdMunicipio equals m.Id
                          join es in db.Estado on m.IdEstado equals es.Id
                          where t.EncuestaRespondida
                          select new { t, es, ug };


                if (grupos.Any())
                    qry = grupos.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (tipoArbol.Any())
                    qry = from q in qry
                          where tipoArbol.Contains(q.t.IdTipoArbolAcceso)
                          select q;
                if (responsables.Any())
                    qry = responsables.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (encuestas.Any())
                    qry = from q in qry
                          where encuestas.Contains(q.t.IdEncuesta)
                          select q;

                if (atendedores.Any())
                    qry = atendedores.Aggregate(qry, (current, grupo) => (from q in current where q.t.TicketGrupoUsuario.Select(s => s.IdGrupoUsuario).Contains(grupo) select q));

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (tiposUsuario.Any())
                    qry = from q in qry
                          where tiposUsuario.Contains(q.t.IdTipoUsuario)
                          select q;

                if (prioridad.Any())
                    qry = from q in qry
                          where prioridad.Contains(q.t.IdImpacto)
                          select q;

                if (sla.Any())
                    qry = from q in qry
                          where sla.Contains(q.t.DentroSla)
                          select q;

                if (ubicaciones.Any())
                    qry = from q in qry
                          where ubicaciones.Contains(q.t.IdUbicacion)
                          select q;

                if (organizaciones.Any())
                    qry = from q in qry
                          where organizaciones.Contains(q.t.IdOrganizacion)
                          select q;

                if (vip.Any())
                    qry = from q in qry
                          where vip.Contains(q.t.UsuarioLevanto.Vip)
                          select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                var lstTickets = (from q in qry.Distinct()
                                  group new { q.es.RegionCode } by new { q.es.RegionCode }
                                      into g
                                      select new { g.Key.RegionCode, Hits = g.Count() }).ToList();

                if (lstTickets.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Region");
                    dt.Columns.Add("Hits");
                    foreach (var data in lstTickets)
                    {
                        dt.Rows.Add(data.RegionCode, data.Hits);
                    }
                    result = dt.Columns.Cast<DataColumn>().Aggregate("[\n[", (current, column) => current + string.Format("'{0}', ", column.ColumnName));
                    result = result.Trim().TrimEnd(',') + "], \n";
                    result = dt.Rows.Cast<DataRow>().Aggregate(result, (current, row) => current + string.Format("['{0}', {1}]\n", row[0], row[1]));
                    result += "]";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public List<DataTable> GraficarConsultaEncuestaPregunta(int idUsuario, int idEncuesta, Dictionary<string, DateTime> fechas, int tipoFecha, int tipoEncuesta)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<DataTable> result;
            DateTime fechaInicio = new DateTime();
            int conteo = 1;
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }
                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join ep in db.EncuestaPregunta on new { idenctick = (int)t.IdEncuesta, ids = e.Id } equals new { idenctick = ep.IdEncuesta, ids = ep.IdEncuesta }
                          join re in db.RespuestaEncuesta on
                          new { idTickTick = t.Id, idEncTick = (int)t.IdEncuesta, idEncPadre = e.Id, idPreg = ep.Id } equals
                          new { idTickTick = re.IdTicket, idEncTick = re.IdEncuesta, idEncPadre = re.IdEncuesta, idPreg = re.IdPregunta }
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          where t.EncuestaRespondida && t.IdEncuesta == idEncuesta
                          select new { t, tgu, ug, e, re, ep };

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                var lstTickets = qry.Distinct().ToList();
                result = new List<DataTable>();
                int row = 0;
                foreach (EncuestaPregunta pregunta in lstTickets.Select(s => s.ep).Distinct())
                {
                    DataTable dt = new DataTable(pregunta.Pregunta);
                    switch (tipoEncuesta)
                    {
                        #region Logico
                        case (int)BusinessVariables.EnumTipoEncuesta.Logica:
                            row = 0;
                            dt.Columns.Add(new DataColumn("Id"));
                            dt.Columns.Add(new DataColumn("Descripcion"));
                            dt.Columns.Add(new DataColumn("Total"));
                            List<string> lstFechasLogica = lstTickets.OrderBy(o => o.t.FechaHoraAlta).Distinct().Select(s => s.t.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                            switch (tipoFecha)
                            {
                                case 1:
                                    foreach (string fecha in lstFechasLogica)
                                    {
                                        dt.Columns.Add(fecha);
                                    }
                                    break;
                                case 2:
                                    foreach (string fecha in lstFechasLogica)
                                    {
                                        if (!dt.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                        conteo++;
                                    }
                                    break;
                                case 3:
                                    foreach (string fecha in lstFechasLogica)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                                    }
                                    break;
                                case 4:
                                    foreach (string fecha in lstFechasLogica)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                                    }
                                    break;
                            }
                            dt.Rows.Add(pregunta.Id, "SI");

                            var totalEncuestas = lstTickets.Select(s => new { s.re.IdTicket, s.re.IdEncuesta }).Where(w => w.IdEncuesta == idEncuesta).Distinct().Count();
                            var ponderacionHundredPorcent = pregunta.Ponderacion * decimal.Parse(totalEncuestas.ToString());
                            var enumeracionLogica = lstTickets.Where(w => w.re.IdPregunta == pregunta.Id && w.re.IdEncuesta == pregunta.IdEncuesta)
                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.ValorRespuesta, s.re.Ponderacion }).Distinct().ToList().Where(w => w.ValorRespuesta == 1).GroupBy(ge => new { ge.IdTicket, ge.IdPregunta, ge.IdEncuesta, ge.Ponderacion })
                            .Distinct().Select(ssum =>
                                    new
                                    {
                                        ssum.Key.IdTicket,
                                        ssum.Key.IdPregunta,
                                        ssum.Key.IdEncuesta,
                                        SumaEncuesta = ssum.Sum(sumas => sumas.Ponderacion)
                                    })
                            .Distinct().ToList();
                            var totalLogica = enumeracionLogica.Count * pregunta.Ponderacion;
                            dt.Rows[row][2] = (totalLogica * 100) / ponderacionHundredPorcent;

                            for (int i = 3; i < dt.Columns.Count; i++)
                            {
                                switch (tipoFecha)
                                {
                                    case 1:
                                        var preguntaEncontradaDiario = lstTickets
                                           .Select(s => new { s.t, s.re })
                                           .Where(c => c.t.FechaHoraAlta.ToString("dd/MM/yyyy") == dt.Columns[i].ColumnName && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 1)
                                           .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                           .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta)
                                           .Distinct().Count();
                                        dt.Rows[row][i] = (preguntaEncontradaDiario * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 2:
                                        var preguntaEncontradaSemanal = lstTickets.Select(s => new { s.t, s.re }).Where(c =>
                                            DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1]))
                                            && DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1]))
                                            && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 1)
                                            .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row][i] = (preguntaEncontradaSemanal * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 3:
                                        var preguntaEncontradaMensual = lstTickets.Select(s => new { s.t, s.re }).Where(c => c.t.FechaHoraAlta.ToString("MM") == dt.Columns[i].ColumnName.PadLeft(2, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 1)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row][i] = (preguntaEncontradaMensual * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 4:
                                        var preguntaEncontradaAnual = lstTickets.Select(s => new { s.t, s.re }).Where(c => c.t.FechaHoraAlta.ToString("yyyy") == dt.Columns[i].ColumnName.PadLeft(4, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 1)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row][i] = (preguntaEncontradaAnual * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                }
                            }
                            dt.Rows.Add(pregunta.Id, "NO");
                            enumeracionLogica = lstTickets.Where(w => w.re.IdPregunta == pregunta.Id && w.re.IdEncuesta == pregunta.IdEncuesta)
                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.ValorRespuesta, s.re.Ponderacion }).Distinct().ToList().Where(w => w.ValorRespuesta == 0).GroupBy(ge => new { ge.IdTicket, ge.IdPregunta, ge.IdEncuesta, ge.Ponderacion })
                            .Distinct().Select(ssum =>
                                    new
                                    {
                                        ssum.Key.IdTicket,
                                        ssum.Key.IdPregunta,
                                        ssum.Key.IdEncuesta,
                                        SumaEncuesta = ssum.Sum(sumas => sumas.Ponderacion)
                                    })
                            .Distinct().ToList();
                            totalLogica = enumeracionLogica.Count * pregunta.Ponderacion;
                            dt.Rows[row + 1][2] = (totalLogica * 100) / ponderacionHundredPorcent;

                            for (int i = 3; i < dt.Columns.Count; i++)
                            {
                                switch (tipoFecha)
                                {
                                    case 1:
                                        var preguntaEncontradaDiario = lstTickets
                                           .Select(s => new { s.t, s.re })
                                           .Where(c => c.t.FechaHoraAlta.ToString("dd/MM/yyyy") == dt.Columns[i].ColumnName && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 0)
                                           .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                           .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta)
                                           .Distinct().Count();
                                        dt.Rows[row + 1][i] = (preguntaEncontradaDiario * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 2:
                                        var preguntaEncontradaSemanal = lstTickets.Select(s => new { s.t, s.re }).Where(c =>
                                            DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1]))
                                            && DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1]))
                                            && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 0).Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row + 1][i] = (preguntaEncontradaSemanal * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 3:
                                        var preguntaEncontradaMensual = lstTickets.Select(s => new { s.t, s.re }).Where(c => c.t.FechaHoraAlta.ToString("MM") == dt.Columns[i].ColumnName.PadLeft(2, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 0)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row + 1][i] = (preguntaEncontradaMensual * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                    case 4:
                                        var preguntaEncontradaAnual = lstTickets.Select(s => new { s.t, s.re }).Where(c => c.t.FechaHoraAlta.ToString("yyyy") == dt.Columns[i].ColumnName.PadLeft(4, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == 0)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion }).Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                        dt.Rows[row + 1][i] = (preguntaEncontradaAnual * pregunta.Ponderacion * 100) / ponderacionHundredPorcent;
                                        break;
                                }
                            }

                            result.Add(dt);
                            break;
                        #endregion Logico
                        #region Calificacion
                        case (int)BusinessVariables.EnumTipoEncuesta.Calificacion:
                            row = 0;
                            dt.Columns.Add(new DataColumn("Id"));
                            dt.Columns.Add(new DataColumn("Descripcion"));
                            dt.Columns.Add(new DataColumn("Total"));
                            List<string> lstFechasCalificacion = lstTickets.OrderBy(o => o.t.FechaHoraAlta).Distinct().Select(s => s.t.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                            switch (tipoFecha)
                            {
                                case 1:
                                    foreach (string fecha in lstFechasCalificacion)
                                    {
                                        dt.Columns.Add(fecha);
                                    }
                                    break;
                                case 2:
                                    foreach (string fecha in lstFechasCalificacion)
                                    {
                                        if (!dt.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                        conteo++;
                                    }
                                    break;
                                case 3:
                                    foreach (string fecha in lstFechasCalificacion)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                                    }
                                    break;
                                case 4:
                                    foreach (string fecha in lstFechasCalificacion)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                                    }
                                    break;
                            }
                            var totalEncuestasCalificacion = lstTickets.Select(s => new { s.re.IdTicket, s.re.IdEncuesta }).Where(w => w.IdEncuesta == idEncuesta).Distinct().Count();
                            var ponderacionHundredPorcentCalificacion = pregunta.Ponderacion * decimal.Parse(totalEncuestasCalificacion.ToString());

                            for (int valRespuesta = 0; valRespuesta < 10; valRespuesta++)
                            {
                                dt.Rows.Add(pregunta.Id, valRespuesta);


                                var enumeracionCalificacion = lstTickets
                                    .Where(w => w.re.IdPregunta == pregunta.Id && w.re.IdEncuesta == pregunta.IdEncuesta && w.re.ValorRespuesta == valRespuesta)
                                        .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.ValorRespuesta, s.re.Ponderacion }).Distinct().ToList()
                                        .Where(w => w.ValorRespuesta == valRespuesta)
                                        .GroupBy(ge => new { ge.IdTicket, ge.IdPregunta, ge.IdEncuesta, ge.Ponderacion })
                                        .Distinct().Select(ssum => new { ssum.Key.IdTicket, ssum.Key.IdPregunta, ssum.Key.IdEncuesta, SumaEncuesta = ssum.Sum(sumas => sumas.Ponderacion) }).Distinct().ToList();

                                var totalCalificacion = enumeracionCalificacion.Count() * pregunta.Ponderacion;
                                dt.Rows[row + valRespuesta][2] = (totalCalificacion * 100) / ponderacionHundredPorcentCalificacion;
                                for (int i = 3; i < dt.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            var preguntaEncontradaDiario = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("dd/MM/yyyy") == dt.Columns[i].ColumnName && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();

                                            dt.Rows[row + valRespuesta][i] = (preguntaEncontradaDiario * pregunta.Ponderacion * 100) / ponderacionHundredPorcentCalificacion;

                                            break;
                                        case 2:
                                            var preguntaEncontradaSemanal = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1])) && DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1])) && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta][i] = (preguntaEncontradaSemanal * pregunta.Ponderacion * 100) / ponderacionHundredPorcentCalificacion;
                                            break;
                                        case 3:
                                            var preguntaEncontradaMensual = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("MM") == dt.Columns[i].ColumnName.PadLeft(2, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta][i] = (preguntaEncontradaMensual * pregunta.Ponderacion * 100) / ponderacionHundredPorcentCalificacion;
                                            break;
                                        case 4:
                                            var preguntaEncontradaAnual = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("yyyy") == dt.Columns[i].ColumnName.PadLeft(4, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta][i] = (preguntaEncontradaAnual * pregunta.Ponderacion * 100) / ponderacionHundredPorcentCalificacion;
                                            break;
                                    }
                                }
                            }
                            result.Add(dt);
                            break;
                        #endregion Calificacion
                        #region Opcional
                        case (int)BusinessVariables.EnumTipoEncuesta.OpcionMultiple:
                            row = 0;
                            dt.Columns.Add(new DataColumn("Id"));
                            dt.Columns.Add(new DataColumn("Descripcion"));
                            dt.Columns.Add(new DataColumn("Total"));
                            List<string> lstFechasOpcional = lstTickets.OrderBy(o => o.t.FechaHoraAlta).Distinct().Select(s => s.t.FechaHoraAlta.ToString("dd/MM/yyyy")).Distinct().ToList();
                            switch (tipoFecha)
                            {
                                case 1:
                                    foreach (string fecha in lstFechasOpcional)
                                    {
                                        dt.Columns.Add(fecha);
                                    }
                                    break;
                                case 2:
                                    foreach (string fecha in lstFechasOpcional)
                                    {
                                        if (!dt.Columns.Contains("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add("SEMANA " + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Parse(fecha), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + " AÑO " + DateTime.Parse(fecha).Year.ToString());
                                        conteo++;
                                    }
                                    break;
                                case 3:
                                    foreach (string fecha in lstFechasOpcional)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Month.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Month.ToString());
                                    }
                                    break;
                                case 4:
                                    foreach (string fecha in lstFechasOpcional)
                                    {
                                        if (!dt.Columns.Contains(DateTime.Parse(fecha).Year.ToString()))
                                            dt.Columns.Add(DateTime.Parse(fecha).Year.ToString());
                                    }
                                    break;
                            }
                            var totalEncuestasOpcional = lstTickets.Select(s => new { s.re.IdTicket, s.re.IdEncuesta }).Where(w => w.IdEncuesta == idEncuesta).Distinct().Count();
                            var ponderacionHundredPorcentOpcional = pregunta.Ponderacion * decimal.Parse(totalEncuestasOpcional.ToString());

                            for (int valRespuesta = 1; valRespuesta < 6; valRespuesta++)
                            {
                                switch (valRespuesta)
                                {
                                    case 1:
                                        dt.Rows.Add(pregunta.Id, "PESIMO");
                                        break;
                                    case 2:
                                        dt.Rows.Add(pregunta.Id, "MALO");
                                        break;
                                    case 3:
                                        dt.Rows.Add(pregunta.Id, "REGULAR");
                                        break;
                                    case 4:
                                        dt.Rows.Add(pregunta.Id, "BUENO");
                                        break;
                                    case 5:
                                        dt.Rows.Add(pregunta.Id, "EXCELENTE");
                                        break;
                                }
                                var enumeracionCalificacion = lstTickets
                                    .Where(w => w.re.IdPregunta == pregunta.Id && w.re.IdEncuesta == pregunta.IdEncuesta && w.re.ValorRespuesta == valRespuesta)
                                        .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.ValorRespuesta, s.re.Ponderacion }).Distinct().ToList()
                                        .Where(w => w.ValorRespuesta == valRespuesta)
                                        .GroupBy(ge => new { ge.IdTicket, ge.IdPregunta, ge.IdEncuesta, ge.Ponderacion })
                                        .Distinct().Select(ssum => new { ssum.Key.IdTicket, ssum.Key.IdPregunta, ssum.Key.IdEncuesta, SumaEncuesta = ssum.Sum(sumas => sumas.Ponderacion) }).Distinct().ToList();

                                var totalOpcional = enumeracionCalificacion.Count() * pregunta.Ponderacion;
                                dt.Rows[row + valRespuesta - 1][2] = (totalOpcional * 100) / ponderacionHundredPorcentOpcional;
                                for (int i = 3; i < dt.Columns.Count; i++)
                                {
                                    switch (tipoFecha)
                                    {
                                        case 1:
                                            var preguntaEncontradaDiario = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("dd/MM/yyyy") == dt.Columns[i].ColumnName && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();

                                            dt.Rows[row + valRespuesta - 1][i] = (preguntaEncontradaDiario * pregunta.Ponderacion * 100) / ponderacionHundredPorcentOpcional;

                                            break;
                                        case 2:
                                            var preguntaEncontradaSemanal = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) >= BusinessCadenas.Fechas.ObtenerFechaInicioSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1])) && DateTime.Parse(c.t.FechaHoraAlta.ToString("dd/MM/yyyy")) <= BusinessCadenas.Fechas.ObtenerFechaFinSemana(int.Parse(dt.Columns[i].ColumnName.Split(' ')[3]), int.Parse(dt.Columns[i].ColumnName.Split(' ')[1])) && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta - 1][i] = (preguntaEncontradaSemanal * pregunta.Ponderacion * 100) / ponderacionHundredPorcentOpcional;
                                            break;
                                        case 3:
                                            var preguntaEncontradaMensual = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("MM") == dt.Columns[i].ColumnName.PadLeft(2, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta - 1][i] = (preguntaEncontradaMensual * pregunta.Ponderacion * 100) / ponderacionHundredPorcentOpcional;
                                            break;
                                        case 4:
                                            var preguntaEncontradaAnual = lstTickets
                                                .Select(s => new { s.t, s.re })
                                                .Where(c => c.t.FechaHoraAlta.ToString("yyyy") == dt.Columns[i].ColumnName.PadLeft(4, '0') && c.t.IdEncuesta == pregunta.IdEncuesta && c.re.ValorRespuesta == valRespuesta)
                                                .Select(s => new { s.re.IdTicket, s.re.IdEncuesta, s.re.IdPregunta, s.re.Ponderacion })
                                                .Where(w => w.IdPregunta == pregunta.Id && w.IdEncuesta == pregunta.IdEncuesta).Distinct().Count();
                                            dt.Rows[row + valRespuesta - 1][i] = (preguntaEncontradaAnual * pregunta.Ponderacion * 100) / ponderacionHundredPorcentOpcional;
                                            break;
                                    }
                                }
                            }
                            result.Add(dt);
                            break;
                        #endregion Logica
                    }
                    row++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public string GraficarConsultaEncuestaPreguntaGeografica(int idUsuario, List<int?> encuestas, Dictionary<string, DateTime> fechas, int tipoFecha)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            string result = null;
            DateTime fechaInicio = new DateTime();
            try
            {
                new BusinessDemonio().ActualizaSla();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                DateTime fechaFin = new DateTime();
                if (fechas != null)
                {
                    fechaInicio = fechas.Single(s => s.Key == "inicio").Value;
                    fechaFin = fechas.Single(s => s.Key == "fin").Value.AddDays(1);
                }

                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join ep in db.EncuestaPregunta on new { idenctick = (int)t.IdEncuesta, ids = e.Id } equals new { idenctick = ep.IdEncuesta, ids = ep.IdEncuesta }
                          join re in db.RespuestaEncuesta on new { idTickTick = t.Id, idEncTick = (int)t.IdEncuesta, idEncPadre = e.Id, idPreg = ep.Id } equals new { idTickTick = re.IdTicket, idEncTick = re.IdEncuesta, idEncPadre = re.IdEncuesta, idPreg = re.IdPregunta }
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join ug in db.UsuarioGrupo on new { tgu.IdGrupoUsuario, tgu.IdSubGrupoUsuario } equals new { ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                          join ub in db.Ubicacion on t.IdUbicacion equals ub.Id
                          join d in db.Domicilio on ub.IdCampus equals d.IdCampus
                          join col in db.Colonia on d.IdColonia equals col.Id
                          join m in db.Municipio on col.IdMunicipio equals m.Id
                          join es in db.Estado on m.IdEstado equals es.Id
                          where t.EncuestaRespondida
                          select new { t, tgu, ug, es };

                if (encuestas.Any())
                    qry = from q in qry
                          where encuestas.Contains(q.t.IdEncuesta)
                          select q;

                if (fechas != null)
                    if (fechas.Count == 2)
                        qry = from q in qry
                              where q.t.FechaHoraAlta >= fechaInicio
                                    && q.t.FechaHoraAlta <= fechaFin
                              select q;

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                var lstTickets = (from q in qry.Distinct()
                                  group new { q.es.RegionCode } by new { q.es.RegionCode }
                                      into g
                                      select new { g.Key.RegionCode, Hits = g.Count() }).ToList();

                if (lstTickets.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Region");
                    dt.Columns.Add("Hits");
                    foreach (var data in lstTickets)
                    {
                        dt.Rows.Add(data.RegionCode, data.Hits);
                    }
                    result = dt.Columns.Cast<DataColumn>().Aggregate("[\n[", (current, column) => current + string.Format("'{0}', ", column.ColumnName));
                    result = result.Trim().TrimEnd(',') + "], \n";
                    result = dt.Rows.Cast<DataRow>().Aggregate(result, (current, row) => current + string.Format("['{0}', {1}]\n", row[0], row[1]));
                    result += "]";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
            return result;
        }
        #endregion Graficas
    }
}
