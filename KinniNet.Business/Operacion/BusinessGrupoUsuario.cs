﻿using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessGrupoUsuario : IDisposable
    {
        private readonly bool _proxy;
        public void Dispose()
        {

        }

        public BusinessGrupoUsuario(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<GrupoUsuario> ObtenerGrupos(bool insertarSeleccion)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.OrderBy(o => o.Descripcion).ToList();
                foreach (GrupoUsuario gpo in result)
                {
                    db.LoadProperty(gpo, "TipoUsuario");
                }
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public List<GrupoUsuario> ObtenerGruposByIdUsuario(int idUsuario, bool insertarSeleccion)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.UsuarioGrupo.Where(w => w.IdUsuario == idUsuario).Select(s => s.GrupoUsuario).Distinct().OrderBy(o => o.Descripcion).ToList();
                foreach (GrupoUsuario gpo in result)
                {
                    db.LoadProperty(gpo, "TipoUsuario");
                }
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public List<GrupoUsuario> ObtenerGruposUsuarioTipoUsuario(int idTipoGrupo, int idTipoUsuario, bool insertarSeleccion)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.Where(w => w.IdTipoGrupo == idTipoGrupo && w.IdTipoUsuario == idTipoUsuario && w.Habilitado)
                        .OrderBy(o => o.Descripcion)
                        .ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public List<GrupoUsuario> ObtenerGruposUsuarioSistema(int idTipoUsuario)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.Where(w => w.Habilitado && w.IdTipoUsuario == idTipoUsuario && w.Sistema && w.IdTipoGrupo != (int)BusinessVariables.EnumTiposGrupos.Administrador)
                        .OrderBy(o => o.Id)
                        .ToList();
                foreach (GrupoUsuario grupo in result)
                {
                    db.LoadProperty(grupo, "TipoGrupo");
                    db.LoadProperty(grupo.TipoGrupo, "RolTipoGrupo");
                    db.LoadProperty(grupo, "SubGrupoUsuario");
                    foreach (SubGrupoUsuario subGrupo in grupo.SubGrupoUsuario)
                    {
                        db.LoadProperty(subGrupo, "SubRol");
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
        public List<GrupoUsuario> ObtenerGruposUsuarioNivel(int idtipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var qry = db.GrupoUsuarioInventarioArbol.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdTipoArbolAcceso == idtipoArbol && w.InventarioArbolAcceso.ArbolAcceso.IdNivel1 == nivel1);
                if (nivel2.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel2 == nivel2);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel2 == null);

                if (nivel3.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel3 == nivel3);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel3 == null);

                if (nivel4.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel4 == nivel4);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel4 == null);

                if (nivel5.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel5 == nivel5);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel5 == null);

                if (nivel6.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel6 == nivel6);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel6 == null);

                if (nivel7.HasValue)
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel7 == nivel7);
                else
                    qry = qry.Where(w => w.InventarioArbolAcceso.ArbolAcceso.IdNivel7 == null);

                result = qry.Select(s => s.GrupoUsuario).Distinct().ToList();

                foreach (GrupoUsuario grupo in result)
                {
                    db.LoadProperty(grupo, "TipoGrupo");
                    db.LoadProperty(grupo.TipoGrupo, "RolTipoGrupo");
                    db.LoadProperty(grupo, "SubGrupoUsuario");
                    foreach (SubGrupoUsuario subGrupo in grupo.SubGrupoUsuario)
                    {
                        db.LoadProperty(subGrupo, "SubRol");
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
        public List<GrupoUsuario> ObtenerGruposUsuarioSistemaNivelArbol()
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //db.GrupoUsuarioInventarioArbol.Where(w => w.InventarioArbolAcceso.ArbolAcceso.Nivel1.Id == 1);
                result = db.GrupoUsuario.Where(w => w.Habilitado && w.Sistema && w.IdTipoGrupo != (int)BusinessVariables.EnumTiposGrupos.Administrador)
                        .OrderBy(o => o.Id)
                        .ToList();
                foreach (GrupoUsuario grupo in result)
                {
                    db.LoadProperty(grupo, "TipoGrupo");
                    db.LoadProperty(grupo.TipoGrupo, "RolTipoGrupo");
                    db.LoadProperty(grupo, "SubGrupoUsuario");
                    foreach (SubGrupoUsuario subGrupo in grupo.SubGrupoUsuario)
                    {
                        db.LoadProperty(subGrupo, "SubRol");
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
        public List<GrupoUsuario> ObtenerGruposUsuarioByIdTipoSubGrupo(int idTipoSubgrupo, bool insertarSeleccion)
        {
            List<GrupoUsuario> result = new List<GrupoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //result = db.SubGrupoUsuario.Where(w => w.Habilitado && w.IdTipoSubGrupo == idTipoSubgrupo).Select(s => s.GrupoUsuario).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRolTipoUsuario(int idRol, int idTipoUsuario, bool insertarSeleccion)
        {
            List<GrupoUsuario> result = new List<GrupoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.RolTipoGrupo.Where(w => w.Rol.Habilitado && w.IdRol == idRol).SelectMany(s => s.TipoGrupo.GrupoUsuario).Where(w => w.IdTipoUsuario == idTipoUsuario).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, bool insertarSeleccion)
        {
            List<GrupoUsuario> result = new List<GrupoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.RolTipoGrupo.Where(w => w.Rol.Habilitado && w.IdRol == idRol).SelectMany(s => s.TipoGrupo.GrupoUsuario).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
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
        public void GuardarGrupoUsuario(GrupoUsuario grupoUsuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                grupoUsuario.Descripcion = grupoUsuario.Descripcion.Trim().ToUpper();
                if (db.GrupoUsuario.Any(a => a.Descripcion == grupoUsuario.Descripcion && a.IdTipoGrupo == grupoUsuario.IdTipoGrupo))
                {
                    throw new Exception("Ya existe un Grupo con esta descripcion");
                }
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el que viene embebido
                grupoUsuario.Habilitado = true;
                grupoUsuario.TieneSupervisor = grupoUsuario.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                if (grupoUsuario.Id == 0)
                {
                    grupoUsuario.EstatusTicketSubRolGeneral = GeneraEstatusGrupoDefault(grupoUsuario);
                    grupoUsuario.EstatusAsignacionSubRolGeneral = GeneraEstatusAsignacionGrupoDefault(grupoUsuario);
                    db.GrupoUsuario.AddObject(grupoUsuario);
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
        public void GuardarGrupoUsuario(GrupoUsuario grupoUsuario, Dictionary<int, int> horarios, Dictionary<int, List<DiaFestivoSubGrupo>> diasDescanso)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                grupoUsuario.Descripcion = grupoUsuario.Descripcion.Trim().ToUpper();
                if (db.GrupoUsuario.Any(a => a.Descripcion == grupoUsuario.Descripcion && a.IdTipoGrupo == grupoUsuario.IdTipoGrupo))
                {
                    throw new Exception("Ya existe un Grupo con esta descripcion");
                }
                grupoUsuario.SubGrupoUsuario = new List<SubGrupoUsuario>();
                foreach (KeyValuePair<int, int> horario in horarios)
                {

                    List<HorarioSubGrupo> lstHorarioGpo = new List<HorarioSubGrupo>();
                    List<HorarioDetalle> detalle = db.HorarioDetalle.Where(w => w.IdHorario == horario.Value).ToList();
                    foreach (HorarioDetalle horarioDetalle in detalle)
                    {
                        HorarioSubGrupo horarioGpo = new HorarioSubGrupo
                        {
                            IdHorario = horario.Value,
                            IdSubGrupoUsuario = horario.Key,
                            Dia = horarioDetalle.Dia,
                            HoraInicio = horarioDetalle.HoraInicio,
                            HoraFin = horarioDetalle.HoraFin
                        };
                        lstHorarioGpo.Add(horarioGpo);
                    }

                    SubGrupoUsuario subGrupo = new SubGrupoUsuario();
                    subGrupo.IdSubRol = horario.Key;
                    subGrupo.Habilitado = true;
                    subGrupo.HorarioSubGrupo = subGrupo.HorarioSubGrupo ?? new List<HorarioSubGrupo>();
                    subGrupo.DiaFestivoSubGrupo = subGrupo.DiaFestivoSubGrupo ?? new List<DiaFestivoSubGrupo>();
                    subGrupo.HorarioSubGrupo.AddRange(lstHorarioGpo);
                    List<DiaFestivoSubGrupo> lstDiasDescanso = diasDescanso.SingleOrDefault(w => w.Key == horario.Key).Value;
                    if (lstDiasDescanso != null)
                    {
                        foreach (DiaFestivoSubGrupo dia in lstDiasDescanso)
                        {
                            dia.IdSubGrupoUsuario = horario.Key;
                        }
                        subGrupo.DiaFestivoSubGrupo.AddRange(lstDiasDescanso);
                    }

                    grupoUsuario.SubGrupoUsuario.Add(subGrupo);
                }
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el que viene embebido
                grupoUsuario.Habilitado = true;
                grupoUsuario.TieneSupervisor = grupoUsuario.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                if (grupoUsuario.Id == 0)
                {
                    grupoUsuario.EstatusTicketSubRolGeneral = GeneraEstatusGrupoDefault(grupoUsuario);
                    grupoUsuario.EstatusAsignacionSubRolGeneral = GeneraEstatusAsignacionGrupoDefault(grupoUsuario);
                    db.GrupoUsuario.AddObject(grupoUsuario);
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
        public GrupoUsuario ObtenerGrupoUsuarioById(int idGrupoUsuario)
        {
            GrupoUsuario result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.SingleOrDefault(s => s.Id == idGrupoUsuario);
                if (result != null)
                {
                    db.LoadProperty(result, "TipoUsuario");
                    db.LoadProperty(result, "TipoGrupo");
                    db.LoadProperty(result, "SubGrupoUsuario");
                    foreach (SubGrupoUsuario sb in result.SubGrupoUsuario)
                    {
                        db.LoadProperty(sb, "HorarioSubGrupo");
                        db.LoadProperty(sb, "DiaFestivoSubGrupo");
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
        public List<UsuarioGrupo> ObtenerGruposDeUsuario(int idUsuario)
        {
            List<UsuarioGrupo> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = ((IQueryable<UsuarioGrupo>)from ug in db.UsuarioGrupo
                                                    join gu in db.GrupoUsuario on ug.IdGrupoUsuario equals gu.Id into joingroups
                                                    from sgu in db.SubGrupoUsuario.Where(w => w.Id == ug.IdSubGrupoUsuario).DefaultIfEmpty()
                                                    from sr in db.SubRol.Where(w => w.Id == sgu.IdSubRol).DefaultIfEmpty()
                                                    where ug.IdUsuario == idUsuario
                                                    select ug).ToList();

                //result = db.UsuarioGrupo.Where(w=>w.IdUsuario == idUsuario).Select(s=>s.GrupoUsuario).ToList();
                foreach (UsuarioGrupo grupo in result)
                {
                    db.LoadProperty(grupo, "GrupoUsuario");
                    db.LoadProperty(grupo.GrupoUsuario, "TipoGrupo");
                    db.LoadProperty(grupo, "SubGrupoUsuario");
                    if (grupo.SubGrupoUsuario != null)
                        db.LoadProperty(grupo.SubGrupoUsuario, "SubRol");
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
        public void HabilitarGrupo(int idGrupo, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                if (db.GrupoUsuario.Single(s => s.Id == idGrupo).IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.Administrador)
                    if (db.GrupoUsuario.Count(w => w.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.Administrador && w.Habilitado && w.Id != idGrupo && w.UsuarioGrupo.Count(ug => ug.IdGrupoUsuario == w.Id) > 0) <= 0)
                    {
                        throw new Exception("Debe tener otro usuario activo para este tipo de grupo.");
                    }
                GrupoUsuario grpo = db.GrupoUsuario.SingleOrDefault(w => w.Id == idGrupo);
                if (grpo != null) grpo.Habilitado = habilitado;
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
        public List<HorarioSubGrupo> ObtenerHorariosByIdSubGrupo(int idSubGrupo)
        {
            List<HorarioSubGrupo> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.HorarioSubGrupo.Where(w => w.IdSubGrupoUsuario == idSubGrupo).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public List<DiaFestivoSubGrupo> ObtenerDiasByIdSubGrupo(int idSubGrupo)
        {
            List<DiaFestivoSubGrupo> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiaFestivoSubGrupo.Where(w => w.IdSubGrupoUsuario == idSubGrupo).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public List<GrupoUsuario> ObtenerGruposUsuarioAll(int? idTipoUsuario, int? idTipoGrupo)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<GrupoUsuario> qry = db.GrupoUsuario;
                if (idTipoUsuario != null)
                    qry = qry.Where(w => w.IdTipoUsuario == idTipoUsuario);

                if (idTipoGrupo != null)
                    qry = qry.Where(w => w.IdTipoGrupo == idTipoGrupo);

                result = qry.ToList();
                foreach (GrupoUsuario grupo in result)
                {
                    db.LoadProperty(grupo, "TipoUsuario");
                    db.LoadProperty(grupo, "TipoGrupo");
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
        public List<GrupoUsuario> ObtenerGruposUsuarioResponsablesByGruposTipoServicio(int idUsuario, List<int> grupos, List<int> tipoServicio)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario, (sgu, ug) => new { sgu, ug })
                        .Any(@t => @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor && @t.ug.IdUsuario == idUsuario);
                var qry = from t in db.Ticket
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join gu in db.GrupoUsuario on tgu.IdGrupoUsuario equals gu.Id
                          join ug in db.UsuarioGrupo on gu.Id equals ug.IdGrupoUsuario
                          where gu.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención || gu.IdTipoUsuario == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeContenido
                          select new { t, gu, ug };

                if (!supervisor)
                    qry = from q in qry
                          where q.ug.IdUsuario == idUsuario
                          select q;

                if (grupos.Any())
                    qry = from q in qry
                          where grupos.Contains(q.gu.Id)
                          select q;
                if (tipoServicio.Any())
                    qry = from q in qry
                          where tipoServicio.Contains(q.t.IdTipoArbolAcceso)
                          select q;

                result = qry.Select(s => s.gu).Distinct().ToList();
                foreach (GrupoUsuario grupo in result)
                {
                    db.LoadProperty(grupo, "TipoUsuario");
                    db.LoadProperty(grupo, "TipoGrupo");
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
        public void ActualizarGrupo(GrupoUsuario gpo, Dictionary<int, int> horarios, Dictionary<int, List<DiaFestivoSubGrupo>> diasDescanso)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                gpo.Descripcion = gpo.Descripcion.Trim().ToUpper();
                    if (db.GrupoUsuario.Any(a => a.Descripcion == gpo.Descripcion && a.IdTipoGrupo == gpo.IdTipoGrupo && a.Id != gpo.Id))
                    {
                        throw new Exception("Ya existe un Grupo con esta descripción");
                    }
                GrupoUsuario grupo = db.GrupoUsuario.SingleOrDefault(w => w.Id == gpo.Id);
                List<SubGrupoUsuario> sb = new List<SubGrupoUsuario>();
                if (grupo != null)
                {
                    
                    if (grupo.IdTipoGrupo == (int) BusinessVariables.EnumTiposGrupos.AgenteUniversal)
                    {
                        grupo.LevantaTicket = gpo.LevantaTicket;
                        grupo.RecadoTicket = gpo.RecadoTicket;
                    }
                    foreach (KeyValuePair<int, int> horario in horarios)
                    {

                        List<HorarioSubGrupo> lstHorarioGpo = new List<HorarioSubGrupo>();
                        List<HorarioDetalle> detalle = db.HorarioDetalle.Where(w => w.IdHorario == horario.Value).ToList();
                        foreach (HorarioDetalle horarioDetalle in detalle)
                        {
                            HorarioSubGrupo horarioGpo = new HorarioSubGrupo
                            {
                                IdHorario = horario.Value,
                                IdSubGrupoUsuario = horario.Key,
                                Dia = horarioDetalle.Dia,
                                HoraInicio = horarioDetalle.HoraInicio,
                                HoraFin = horarioDetalle.HoraFin
                            };
                            lstHorarioGpo.Add(horarioGpo);
                        }

                        SubGrupoUsuario subGrupo = new SubGrupoUsuario();
                        var h = grupo.SubGrupoUsuario.FirstOrDefault(f => f.IdSubRol == horario.Key);
                        subGrupo.Id = grupo.SubGrupoUsuario.FirstOrDefault(f => f.IdSubRol == horario.Key) != null ? grupo.SubGrupoUsuario.First(f => f.IdSubRol == horario.Key).Id : 0;
                        subGrupo.IdGrupoUsuario = grupo.Id;
                        subGrupo.IdSubRol = horario.Key;
                        subGrupo.Habilitado = true;
                        subGrupo.HorarioSubGrupo = subGrupo.HorarioSubGrupo ?? new List<HorarioSubGrupo>();
                        subGrupo.DiaFestivoSubGrupo = subGrupo.DiaFestivoSubGrupo ?? new List<DiaFestivoSubGrupo>();
                        subGrupo.HorarioSubGrupo.AddRange(lstHorarioGpo);
                        List<DiaFestivoSubGrupo> lstDiasDescanso = diasDescanso.SingleOrDefault(w => w.Key == horario.Key).Value;
                        foreach (DiaFestivoSubGrupo dia in lstDiasDescanso)
                        {
                            dia.IdSubGrupoUsuario = horario.Key;
                        }
                        subGrupo.DiaFestivoSubGrupo.AddRange(lstDiasDescanso);

                        sb.Add(subGrupo);
                    }

                    List<SubGrupoUsuario> sbGpoRemove = (grupo.SubGrupoUsuario.Select(sbGpo => new { sbGpo, sbDelete = sb.SingleOrDefault(s => s.IdGrupoUsuario == sbGpo.IdGrupoUsuario && s.IdSubRol == sbGpo.IdSubRol) }).Where(@t => @t.sbDelete == null).Select(@t => @t.sbGpo)).ToList();
                    foreach (SubGrupoUsuario subGrupoUsuario in sbGpoRemove)
                    {
                        db.SubGrupoUsuario.DeleteObject(subGrupoUsuario);
                    }

                    foreach (SubGrupoUsuario sbGpoAdd in sb)
                    {
                        if (!grupo.SubGrupoUsuario.Any(a => a.IdGrupoUsuario == sbGpoAdd.IdGrupoUsuario && a.IdSubRol == sbGpoAdd.IdSubRol))
                            grupo.SubGrupoUsuario.Add(sbGpoAdd);
                    }
                    List<DiaFestivoSubGrupo> diasEliminar = new List<DiaFestivoSubGrupo>();
                    foreach (SubGrupoUsuario sgu in sb)
                    {
                        diasEliminar.AddRange(db.DiaFestivoSubGrupo.Where(w => w.IdSubGrupoUsuario == sgu.Id));
                    }
                    //foreach (SubGrupoUsuario sgu in sb)
                    //{
                    //    SubGrupoUsuario sgu1 = sgu;
                    //    foreach (DiaFestivoSubGrupo diaNuevo in db.DiaFestivoSubGrupo.Where(w => w.IdSubGrupoUsuario == sgu1.Id))
                    //    {
                    //            diasEliminar.Add(diaNuevo);
                    //    }
                    //}
                    

                    List<HorarioSubGrupo> horariosEliminar = new List<HorarioSubGrupo>();
                    foreach (SubGrupoUsuario sgu in sb)
                    {
                        horariosEliminar.AddRange(db.HorarioSubGrupo.Where(w => w.IdSubGrupoUsuario == sgu.Id));
                    }

                    foreach (DiaFestivoSubGrupo diaEliminar in diasEliminar)
                    {
                        db.DiaFestivoSubGrupo.DeleteObject(diaEliminar);
                    }

                    foreach (HorarioSubGrupo horarioEliminar in horariosEliminar)
                    {
                        db.HorarioSubGrupo.DeleteObject(horarioEliminar);
                    }

                    foreach (SubGrupoUsuario sbGpoDias in sb)
                    {
                        if (sbGpoDias.DiaFestivoSubGrupo != null)
                            foreach (DiaFestivoSubGrupo diaFestivoSubGrupo in sbGpoDias.DiaFestivoSubGrupo)
                            {
                                if (!grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoDias.IdGrupoUsuario && w.IdSubRol == sbGpoDias.IdSubRol).DiaFestivoSubGrupo.Any(a => a.Fecha == diaFestivoSubGrupo.Fecha))
                                    grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoDias.IdGrupoUsuario && w.IdSubRol == sbGpoDias.IdSubRol).DiaFestivoSubGrupo.Add(diaFestivoSubGrupo);
                            }
                    }

                    foreach (SubGrupoUsuario sbGpoHorario in sb)
                    {
                        if (sbGpoHorario.HorarioSubGrupo != null)
                            foreach (HorarioSubGrupo horarioFestivoSubGrupo in sbGpoHorario.HorarioSubGrupo)
                            {
                                if (!grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoHorario.IdGrupoUsuario && w.IdSubRol == sbGpoHorario.IdSubRol).HorarioSubGrupo.Any(a => a.Dia == horarioFestivoSubGrupo.Dia && a.HoraInicio == horarioFestivoSubGrupo.HoraInicio && a.HoraFin == horarioFestivoSubGrupo.HoraFin))
                                    grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoHorario.IdGrupoUsuario && w.IdSubRol == sbGpoHorario.IdSubRol).HorarioSubGrupo.Add(horarioFestivoSubGrupo);
                                else
                                {
                                    grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoHorario.IdGrupoUsuario && w.IdSubRol == sbGpoHorario.IdSubRol).HorarioSubGrupo.Single(a => a.Dia == horarioFestivoSubGrupo.Dia).HoraInicio = horarioFestivoSubGrupo.HoraInicio;
                                    grupo.SubGrupoUsuario.Single(w => w.IdGrupoUsuario == sbGpoHorario.IdGrupoUsuario && w.IdSubRol == sbGpoHorario.IdSubRol).HorarioSubGrupo.Single(a => a.Dia == horarioFestivoSubGrupo.Dia).HoraFin = horarioFestivoSubGrupo.HoraFin;
                                }

                            }
                    }
                    grupo.TieneSupervisor = grupo.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                    grupo.Descripcion = gpo.Descripcion.Trim().ToUpper();
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


        private List<EstatusTicketSubRolGeneral> GeneraEstatusGrupoDefault(GrupoUsuario grupo)
        {

            List<EstatusTicketSubRolGeneral> result = new List<EstatusTicketSubRolGeneral>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                result.AddRange(from subgpo in grupo.SubGrupoUsuario
                                where subgpo != null
                                from statusDefault in db.EstatusTicketSubRolGeneralDefault.Where(w => w.IdSubRolSolicita == subgpo.IdSubRol && w.TieneSupervisor == grupo.TieneSupervisor)
                                select new EstatusTicketSubRolGeneral
                                {
                                    IdRolSolicita = statusDefault.IdRolSolicita,
                                    IdSubRolSolicita = statusDefault.IdSubRolSolicita,
                                    IdRolPertenece = statusDefault.IdRolPertenece,
                                    IdSubRolPertenece = statusDefault.IdSubRolPertenece,
                                    IdEstatusTicketActual = statusDefault.IdEstatusTicketActual,
                                    IdEstatusTicketAccion = statusDefault.IdEstatusTicketAccion,
                                    TieneSupervisor = statusDefault.TieneSupervisor,
                                    Propietario = statusDefault.Propietario,
                                    LevantaTicket = statusDefault.LevantaTicket,
                                    Habilitado = statusDefault.Habilitado
                                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }
        private List<EstatusAsignacionSubRolGeneral> GeneraEstatusAsignacionGrupoDefault(GrupoUsuario grupo)
        {
            List<EstatusAsignacionSubRolGeneral> result = new List<EstatusAsignacionSubRolGeneral>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                result.AddRange(from subgpo in grupo.SubGrupoUsuario
                                where subgpo != null
                                from statusDefault in db.EstatusAsignacionSubRolGeneralDefault.Where(w => w.IdSubRol == subgpo.IdSubRol && w.TieneSupervisor == grupo.TieneSupervisor)
                                select new EstatusAsignacionSubRolGeneral
                                {
                                    IdRol = statusDefault.IdRol,
                                    IdSubRol = statusDefault.IdSubRol,
                                    IdEstatusAsignacionActual = statusDefault.IdEstatusAsignacionActual,
                                    IdEstatusAsignacionAccion = statusDefault.IdEstatusAsignacionAccion,
                                    TieneSupervisor = statusDefault.TieneSupervisor,
                                    Propietario = statusDefault.Propietario,
                                    Habilitado = statusDefault.Habilitado
                                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }
    }
}
