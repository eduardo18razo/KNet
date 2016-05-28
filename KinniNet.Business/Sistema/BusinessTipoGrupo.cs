using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessTipoGrupo: IDisposable
    {
        private bool _proxy;
        public BusinessTipoGrupo(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion)
        {
            List<TipoGrupo> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoGrupo.ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new TipoGrupo
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

        public List<TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion)
        {
            List<TipoGrupo> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result =
                    db.RolTipoGrupo.Where(w => w.IdRol == idrol)
                        .Select(s => s.TipoGrupo)
                        .OrderBy(o => o.Descripcion)
                        .ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new TipoGrupo
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
