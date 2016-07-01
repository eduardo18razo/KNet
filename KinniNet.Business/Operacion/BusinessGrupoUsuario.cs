using System;
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
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessGrupoUsuario(bool proxy = false)
        {
            _proxy = proxy;
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
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
                        });
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
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
                throw new Exception((ex.InnerException).Message);
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
                throw new Exception((ex.InnerException).Message);
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
                throw new Exception((ex.InnerException).Message);
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
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
                        });
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, int idTipoUsuario, bool insertarSeleccion)
        {
            List<GrupoUsuario> result = new List<GrupoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.RolTipoGrupo.Where(w => w.Rol.Habilitado && w.IdRol == idRol).SelectMany(s => s.TipoGrupo.GrupoUsuario).Where(w => w.IdTipoUsuario == idTipoUsuario).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new GrupoUsuario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
                        });
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
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

                grupoUsuario.Descripcion = grupoUsuario.Descripcion.ToUpper();
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
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public GrupoUsuario ObtenerGrupoUsuario(int idGrupoUsuario)
        {
            GrupoUsuario result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.SingleOrDefault(s => s.Id == idGrupoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
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
                    db.LoadProperty(grupo, "SubGrupoUsuario");
                    if (grupo.SubGrupoUsuario != null)
                        db.LoadProperty(grupo.SubGrupoUsuario, "SubRol");
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        private List<EstatusTicketSubRolGeneral> GeneraEstatusGrupoDefault(GrupoUsuario grupo)
        {
            List<EstatusTicketSubRolGeneral> result = null;
            try
            {
                result = new List<EstatusTicketSubRolGeneral>();
                foreach (SubGrupoUsuario subGpo in grupo.SubGrupoUsuario)
                {
                    switch (subGpo.IdSubRol)
                    {
                        case (int)BusinessVariables.EnumSubRoles.Supervisor:
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            //No propietario
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            break;
                        case (int)BusinessVariables.EnumSubRoles.PrimererNivel:
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            //No propietario
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            break;
                        case (int)BusinessVariables.EnumSubRoles.SegundoNivel:
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, Propietario = true, Habilitado = false });
                            //No propietario
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            break;
                        case (int)BusinessVariables.EnumSubRoles.TercerNivel:
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            //No propietario
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, Propietario = false, Habilitado = false });
                            break;
                        case (int)BusinessVariables.EnumSubRoles.CuartoNivel:
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = true, Habilitado = false });
                            //No propietario
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto, Orden = 1, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cancelado, Orden = 2, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReTipificado, Orden = 3, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto, Orden = 4, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto, Orden = 5, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = true });
                            result.Add(new EstatusTicketSubRolGeneral { IdSubRol = subGpo.IdSubRol, IdEstatusTicket = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado, Orden = 6, TieneSupervisor = grupo.TieneSupervisor, Propietario = false, Habilitado = false });
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                                from statusDefault in db.EstatusAsignacionSubRolGeneralDefault.Where(w => w.IdSubRol == subgpo.IdSubRol)
                                select new EstatusAsignacionSubRolGeneral
                                {
                                    IdSubRol = statusDefault.IdSubRol,
                                    IdEstatusAsignacionActual = statusDefault.IdEstatusAsignacionActual,
                                    IdEstatusAsignacionAccion = statusDefault.IdEstatusAsignacionAccion,
                                    Orden = statusDefault.Orden,
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
