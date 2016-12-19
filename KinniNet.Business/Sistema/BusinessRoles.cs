using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessRoles: IDisposable
    {
        private bool _proxy;
        public BusinessRoles(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }
        public List<Rol> ObtenerRoles(int idTipoUsuario, bool insertarSeleccion)
        {
            List<Rol> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result =
                    db.RolTipoUsuario.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Rol.Habilitado)
                        .OrderBy(o => o.Rol.Descripcion)
                        .Select(s => s.Rol).Distinct()
                        .ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Rol
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
}
