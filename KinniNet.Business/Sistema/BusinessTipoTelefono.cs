using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessTipoTelefono : IDisposable
    {
        private bool _proxy;
        public BusinessTipoTelefono(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }
        public List<TipoTelefono> ObtenerTiposTelefono(bool insertarSeleccion)
        {
            List<TipoTelefono> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoTelefono.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new TipoTelefono { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
