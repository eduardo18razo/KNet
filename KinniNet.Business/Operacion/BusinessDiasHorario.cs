using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessDiasHorario : IDisposable
    {
        private readonly bool _proxy;
        public BusinessDiasHorario(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        {

        }

        public List<Horario> ObtenerHorarioSistema(bool insertarSeleccion)
        {
            List<Horario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Horario.Where(w => w.Habilitado).OrderBy(o=>o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new Horario
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
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

        public List<Horario> ObtenerHorarioConsulta(int? idGrupoSolicito)
        {
            List<Horario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Horario> qry = db.Horario;
                if (idGrupoSolicito != null)
                    qry = qry.Where(w => w.IdGrupoSolicito == idGrupoSolicito);
                result = qry.ToList();
                foreach (Horario puesto in result)
                {
                    db.LoadProperty(puesto, "GrupoUsuario");
                }
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

        public void CrearHorario(Horario horario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                horario.Descripcion = horario.Descripcion.ToUpper();
                if (db.Horario.Any(a=>a.Descripcion == horario.Descripcion))
                    throw new Exception("Ya existe un horario con esta descripción");
                db.Horario.AddObject(horario);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public void Habilitar(int idHorario, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Horario inf = db.Horario.SingleOrDefault(w => w.Id == idHorario);
                if (inf != null) inf.Habilitado = habilitado;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

    }
}
