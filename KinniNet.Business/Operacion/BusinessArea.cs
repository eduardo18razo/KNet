using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessArea : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessArea(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<Area> ObtenerAreasUsuario(int idUsuario, bool insertarSeleccion)
        {
            List<Area> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = (from a in db.Area
                    join aa in db.ArbolAcceso on a.Id equals aa.IdArea
                    join iaa in db.InventarioArbolAcceso on aa.Id equals iaa.IdArbolAcceso
                    join guia in db.GrupoUsuarioInventarioArbol on iaa.Id equals guia.IdInventarioArbolAcceso
                    join gu in db.GrupoUsuario on guia.IdGrupoUsuario equals gu.Id
                    join ug in db.UsuarioGrupo on gu.Id equals ug.IdGrupoUsuario
                    where ug.IdUsuario == idUsuario && guia.IdRol == (int)BusinessVariables.EnumRoles.Acceso
                    select a).Distinct().ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Area
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
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

        public List<Area> ObtenerAreasUsuarioTercero(int idUsuario, int idUsuarioTercero, bool insertarSeleccion)
        {
            List<Area> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = (from a in db.Area
                          join aa in db.ArbolAcceso on a.Id equals aa.IdArea
                          join iaa in db.InventarioArbolAcceso on aa.Id equals iaa.IdArbolAcceso
                          join guia in db.GrupoUsuarioInventarioArbol on iaa.Id equals guia.IdInventarioArbolAcceso
                          join gu in db.GrupoUsuario on guia.IdGrupoUsuario equals gu.Id
                          join ug in db.UsuarioGrupo on gu.Id equals ug.IdGrupoUsuario
                          where ug.IdUsuario == idUsuario && ug.IdUsuario == idUsuarioTercero
                          select a).Distinct().ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Area
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
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

        public List<Area> ObtenerAreasUsuarioPublico(bool insertarSeleccion)
        {

            {
                List<Area> result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    result = (from a in db.Area
                              join aa in db.ArbolAcceso on a.Id equals aa.IdArea
                              where BusinessVariables.IdsPublicos.Contains(aa.IdTipoUsuario)
                              select a).Distinct().ToList();
                    if (insertarSeleccion)
                        result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                            new Area
                            {
                                Id = BusinessVariables.ComboBoxCatalogo.Value,
                                Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
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
        }

        public List<Area> ObtenerAreasTipoUsuario(int idTipoUsuario, bool insertarSeleccion)
        {

            {
                List<Area> result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    result = (from a in db.Area
                              join aa in db.ArbolAcceso on a.Id equals aa.IdArea
                              where aa.IdTipoUsuario == idTipoUsuario 
                              select a).Distinct().ToList();
                    if (insertarSeleccion)
                        result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                            new Area
                            {
                                Id = BusinessVariables.ComboBoxCatalogo.Value,
                                Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
                            });
                    if (result.Count <= 0)
                        result = null;
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

        public List<Area> ObtenerAreas(bool insertarSeleccion)
        {
            List<Area> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Area.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Area
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
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

        public void Guardar(Area area)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                area.Habilitado = true;
                area.Descripcion = area.Descripcion.Trim().ToUpper();
                if (area.Id == 0)
                    db.Area.AddObject(area);
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

    }
}
