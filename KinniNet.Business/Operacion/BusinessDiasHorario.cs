using System;
using System.Collections.Generic;
using System.Globalization;
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
        #region Horarios
        public List<Horario> ObtenerHorarioSistema(bool insertarSeleccion)
        {
            List<Horario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Horario.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
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

        public List<Horario> ObtenerHorarioConsulta(string filtro)
        {
            List<Horario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Horario.Where(w => w.Descripcion.Contains(filtro)).ToList();
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
                horario.Descripcion = horario.Descripcion.Trim();
                horario.FechaAlta = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                horario.Habilitado = true;
                if (db.Horario.Any(a => a.Descripcion == horario.Descripcion))
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

        public void Actualizar(Horario horario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                horario.Descripcion = horario.Descripcion.Trim();
                horario.FechaModificacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                if (db.Horario.Any(a => a.Descripcion == horario.Descripcion))
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

        public Horario ObtenerHorarioById(int idHorario)
        {
            Horario result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Horario.SingleOrDefault(s => s.Id == idHorario);
                if (result != null)
                    db.LoadProperty(result, "HorarioDetalle");
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
        #endregion Horarios

        #region Dias Feriados

        public List<DiasFeriados> ObtenerDiasFeriadosConsulta(string filtro)
        {
            List<DiasFeriados> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiasFeriados.Where(w => w.Descripcion.Contains(filtro)).ToList();
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
        public List<DiaFeriado> ObtenerDiasFeriados(bool insertarSeleccion)
        {
            List<DiaFeriado> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiaFeriado.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new DiaFeriado
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

        public List<DiaFestivoDefault> ObtenerDiasDefault(bool insertarSeleccion)
        {
            List<DiaFestivoDefault> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiaFestivoDefault.Where(w => w.Habilitado).OrderBy(o => o.Mes).ThenBy(t => t.Dia).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new DiaFestivoDefault
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

        public void AgregarDiaFeriado(DiaFeriado dia)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                dia.Descripcion = dia.Descripcion.Trim();
                dia.Habilitado = true;
                if (dia.Id == 0)
                {
                    if (db.DiaFeriado.Any(a => a.Fecha == dia.Fecha))
                        throw new Exception("Esta fecha ya fue registrada anteriormente.");
                    if (db.DiaFeriado.Any(a => a.Descripcion == dia.Descripcion.Trim()))
                        throw new Exception("Esta Descripción ya fue registrada anteriormente.");
                    db.DiaFeriado.AddObject(dia);
                }
                else
                {
                    if (db.DiaFeriado.Any(a => a.Fecha == dia.Fecha && a.Id != dia.Id))
                        throw new Exception("Esta fecha ya fue registrada anteriormente.");
                    if (db.DiaFeriado.Any(a => a.Descripcion == dia.Descripcion.Trim() && a.Id != dia.Id))
                        throw new Exception("Esta Descripción ya fue registrada anteriormente.");
                    DiaFeriado diaF = db.DiaFeriado.SingleOrDefault(s => s.Id == dia.Id);
                    if (diaF != null)
                    {
                        diaF.Descripcion = dia.Descripcion;
                        diaF.Fecha = dia.Fecha;
                    }
                }
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

        public DiaFeriado ObtenerDiaByFecha(DateTime fecha)
        {
            DiaFeriado result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiaFeriado.SingleOrDefault(w => w.Fecha == fecha);
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

        public DiaFeriado ObtenerDiaFeriado(int id)
        {
            DiaFeriado result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiaFeriado.SingleOrDefault(w => w.Id == id && w.Habilitado);
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

        public void ActualizarDiasFestivos(DiasFeriados item)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                DiasFeriados diadb = db.DiasFeriados.SingleOrDefault(s => s.Id == item.Id);
                if (diadb != null)
                {
                    diadb.Descripcion = item.Descripcion;
                    diadb.FechaModificacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                    foreach (DiasFeriadosDetalle detalle in diadb.DiasFeriadosDetalle)
                    {
                        db.DiasFeriadosDetalle.DeleteObject(detalle);
                    }
                    diadb.DiasFeriadosDetalle = item.DiasFeriadosDetalle;
                    db.SaveChanges();
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
        }
        public void CrearDiasFestivos(DiasFeriados item)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                item.Descripcion = item.Descripcion.Trim();
                item.Habilitado = true;
                foreach (DiasFeriadosDetalle detalle in item.DiasFeriadosDetalle)
                {
                    detalle.Descripcion = detalle.Descripcion.Trim();
                    detalle.Habilitado = true;
                }
                db.DiasFeriados.AddObject(item);
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

        public List<DiasFeriados> ObtenerDiasFeriadosUser(bool insertarSeleccion)
        {
            List<DiasFeriados> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiasFeriados.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new DiasFeriados
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
        public DiasFeriados ObtenerDiasFeriadosUserById(int id)
        {
            DiasFeriados result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.DiasFeriados.SingleOrDefault(w => w.Id == id && w.Habilitado);
                if (result != null)
                    db.LoadProperty(result, "DiasFeriadosDetalle");
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
        #endregion Dias Feriados
    }
}
