using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessSla : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessSla(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<SLA> ObtenerSla(bool insertarSeleccion)
        {
            List<SLA> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.SLA.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new SLA
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

        public void Guardar(SLA sla)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                sla.Habilitado = true;
                sla.Descripcion = sla.Descripcion.ToUpper();
                if (sla.Id == 0)
                    db.SLA.AddObject(sla);
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
    }
}
