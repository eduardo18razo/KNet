using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessEncuesta : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessEncuesta(bool proxy = false)
        {
            _proxy = proxy;
        }

        public List<Encuesta> ObtenerEncuestas(bool insertarSeleccion)
        {
            List<Encuesta> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Encuesta.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Encuesta
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

        public Encuesta ObtenerEncuesta(int idencuesta)
        {
            Encuesta result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Encuesta.SingleOrDefault(w => w.Id == idencuesta);
                if (result != null)
                {
                    db.LoadProperty(result, "EncuestaPregunta");
                    //foreach (EncuestaPregunta pregunta in result.EncuestaPregunta)
                    //{
                    //    db.LoadProperty(pregunta, "Pregunta");
                    //}
                }
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

        public void GuardarEncuesta(Encuesta encuesta)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                encuesta.Descripcion = encuesta.Descripcion.ToUpper();
                //TODO: Cambiar habilitado por el embebido
                encuesta.Habilitado = true;
                if (encuesta.Id == 0)
                    db.Encuesta.AddObject(encuesta);
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

        public List<Encuesta> Consulta(string descripcion)
        {
            List<Encuesta> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Encuesta> qry = db.Encuesta;
                if (descripcion.Trim() != string.Empty)
                    qry = qry.Where(w => w.Descripcion.Contains(descripcion));
                result = qry.ToList();
                foreach (Encuesta encuesta in result)
                {
                    db.LoadProperty(encuesta, "TipoEncuesta");
                }
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

        public void HabilitarEncuesta(int idencuesta, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Encuesta encuesta = db.Encuesta.SingleOrDefault(w => w.Id == idencuesta);
                if (encuesta != null) encuesta.Habilitado = habilitado;
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
