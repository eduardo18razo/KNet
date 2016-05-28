using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessCatalogos : IDisposable
    {
        private bool _proxy;
        public BusinessCatalogos(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion)
        {
            List<Catalogos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Catalogos.Where(w => w.EsMascaraCaptura && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Catalogos
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
