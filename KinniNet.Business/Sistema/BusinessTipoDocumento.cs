using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessTipoDocumento : IDisposable
    {
        private bool _proxy;
        public BusinessTipoDocumento(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<TipoDocumento> ObtenerTipoDocumentos(bool insertarSeleccion)
        {
            List<TipoDocumento> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoDocumento.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new TipoDocumento
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

        public TipoDocumento ObtenerTiposDocumentoId(int idTipoDocumento)
        {
            TipoDocumento result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoDocumento.SingleOrDefault(w => w.Id == idTipoDocumento && w.Habilitado);
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
