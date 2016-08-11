using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessPuesto : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessPuesto(bool proxy = false)
        {
            _proxy = proxy;
        }

        public List<Puesto> ObtenerPuestos(bool insertarSeleccion)
        {
            List<Puesto> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Puesto.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Puesto
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

        public void Guardar(Puesto puesto)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                puesto.Habilitado = true;
                puesto.Descripcion = puesto.Descripcion.Trim().ToUpper();
                if (puesto.Id == 0)
                    db.Puesto.AddObject(puesto);
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
