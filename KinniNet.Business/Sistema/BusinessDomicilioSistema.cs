using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones.Domicilio;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessDomicilioSistema : IDisposable
    {
        private bool _proxy;
        public BusinessDomicilioSistema(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<Colonia> ObtenerColoniasCp(int cp, bool insertarSeleccion)
        {
            List<Colonia> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Colonia.Where(w => w.CP == cp).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Colonia { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion });
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
