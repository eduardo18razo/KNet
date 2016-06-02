using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
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
        public List<GrupoUsuario> ObtenerGruposUsuario(int idTipoGrupo, bool insertarSeleccion)
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.GrupoUsuario.Where(w => w.IdTipoGrupo == idTipoGrupo && w.Habilitado && w.Sistema)
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

        public List<GrupoUsuario> ObtenerGruposUsuarioSistema()
        {
            List<GrupoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
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

                result = qry.Select(s=>s.GrupoUsuario).ToList();
                
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
                db.GrupoUsuarioInventarioArbol.Where(w => w.InventarioArbolAcceso.ArbolAcceso.Nivel1.Id == 1);
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

        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, bool insertarSeleccion)
        {
            List<GrupoUsuario> result = new List<GrupoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.RolTipoGrupo.Where(w => w.Rol.Habilitado && w.IdRol == idRol).SelectMany(s => s.TipoGrupo.GrupoUsuario).ToList();
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
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el que viene embebido
                grupoUsuario.Habilitado = true;
                if (grupoUsuario.Id == 0)
                    db.GrupoUsuario.AddObject(grupoUsuario);
                else
                {
                    GrupoUsuario tmpJefatura = db.GrupoUsuario.SingleOrDefault(s => s.Id == grupoUsuario.Id);
                    if (tmpJefatura == null) return;
                    tmpJefatura.IdTipoGrupo = grupoUsuario.IdTipoGrupo;
                    tmpJefatura.Descripcion = grupoUsuario.Descripcion;
                    tmpJefatura.Habilitado = grupoUsuario.Habilitado;
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


    }
}
