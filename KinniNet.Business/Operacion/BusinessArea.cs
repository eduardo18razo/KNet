using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
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
        public List<Area> ObtenerAreasUsuario(int idUsuario)
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
                    where ug.IdUsuario == idUsuario
                    select a).Distinct().ToList();
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

        public List<Area> ObtenerAreasUsuarioPublico(int idTipoUsuario)
        {
            return null;
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
