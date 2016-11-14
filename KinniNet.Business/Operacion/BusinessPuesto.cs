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

        public void Actualizar(int idPuesto, Puesto puesto)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Puesto pto = db.Puesto.SingleOrDefault(s => s.Id == idPuesto);
                if (pto == null) return;
                pto.Descripcion = puesto.Descripcion.Trim().ToUpper();

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

        public List<Puesto> ObtenerPuestoConsulta(int? idPuesto)
        {
            List<Puesto> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Puesto> qry = db.Puesto;
                if (idPuesto != null)
                    qry = qry.Where(w => w.Id == idPuesto);
                result = qry.ToList();
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

        public void Habilitar(int idPuesto, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Puesto inf = db.Puesto.SingleOrDefault(w => w.Id == idPuesto);
                if (inf != null) inf.Habilitado = habilitado;
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
