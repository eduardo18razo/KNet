using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones.Domicilio;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessUbicacion : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessUbicacion(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<Pais> ObtenerPais(int idTipoUsuario, bool insertarSeleccion)
        {
            List<Pais> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Pais.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Pais { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<Campus> ObtenerCampus(int idTipoUsuario, int idPais, bool insertarSeleccion)
        {
            List<Campus> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdPais == idPais).SelectMany(ubicacion => db.Campus.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdCampus && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Campus { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<Torre> ObtenerTorres(int idTipoUsuario, int idCampus, bool insertarSeleccion)
        {
            List<Torre> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdCampus == idCampus).SelectMany(ubicacion => db.Torre.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdTorre && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Torre { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<Piso> ObtenerPisos(int idTipoUsuario, int idTorre, bool insertarSeleccion)
        {
            List<Piso> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdTorre == idTorre).SelectMany(ubicacion => db.Piso.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdPiso && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Piso { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<Zona> ObtenerZonas(int idTipoUsuario, int idPiso, bool insertarSeleccion)
        {
            List<Zona> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdPiso == idPiso).SelectMany(ubicacion => db.Zona.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdZona && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Zona { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<SubZona> ObtenerSubZonas(int idTipoUsuario, int idZona, bool insertarSeleccion)
        {
            List<SubZona> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdZona == idZona).SelectMany(ubicacion => db.SubZona.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdSubZona && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new SubZona { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<SiteRack> ObtenerSiteRacks(int idTipoUsuario, int idSubZona, bool insertarSeleccion)
        {
            List<SiteRack> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdSubZona == idSubZona).SelectMany(ubicacion => db.SiteRack.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdSiteRack && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new SiteRack { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public Ubicacion ObtenerUbicacion(int idPais, int? idCampus, int? idTorre, int? idPiso, int? idZona, int? idSubZona, int? idSiteRack)
        {
            Ubicacion result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var qry = db.Ubicacion.Where(w => w.IdPais == idPais);
                if (idCampus.HasValue)
                    qry = qry.Where(w => w.IdCampus == idCampus);
                else
                    qry = qry.Where(w => w.IdCampus == null);

                if (idTorre.HasValue)
                    qry = qry.Where(w => w.IdTorre == idTorre);
                else
                    qry = qry.Where(w => w.IdTorre == null);

                if (idPiso.HasValue)
                    qry = qry.Where(w => w.IdPiso == idPiso);
                else
                    qry = qry.Where(w => w.IdPiso == null);

                if (idZona.HasValue)
                    qry = qry.Where(w => w.IdZona == idZona);
                else
                    qry = qry.Where(w => w.IdZona == null);

                if (idSubZona.HasValue)
                    qry = qry.Where(w => w.IdSubZona == idSubZona);
                else
                    qry = qry.Where(w => w.IdSubZona == null);

                if (idSiteRack.HasValue)
                    qry = qry.Where(w => w.IdSiteRack == idSiteRack);
                else
                    qry = qry.Where(w => w.IdSiteRack == null);

                result = qry.FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Error al Obtener Organizacion");
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public void GuardarUbicacion(Ubicacion ubicacion)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                ubicacion.Habilitado = true;
                if (ubicacion.Campus != null)
                {
                    ubicacion.Campus.Descripcion = ubicacion.Campus.Descripcion.ToUpper();
                    foreach (Domicilio domicilio in ubicacion.Campus.Domicilio)
                    {
                        domicilio.Calle = domicilio.Calle.ToUpper();
                        domicilio.NoExt = domicilio.NoExt.ToUpper();
                        domicilio.NoInt = domicilio.NoInt.ToUpper();
                    }
                }
                if (ubicacion.Torre != null)
                    ubicacion.Torre.Descripcion = ubicacion.Torre.Descripcion.ToUpper();

                if (ubicacion.Piso != null)
                    ubicacion.Piso.Descripcion = ubicacion.Piso.Descripcion.ToUpper();

                if (ubicacion.Zona != null)
                    ubicacion.Zona.Descripcion = ubicacion.Zona.Descripcion.ToUpper();

                if (ubicacion.SubZona != null)
                    ubicacion.SubZona.Descripcion = ubicacion.SubZona.Descripcion.ToUpper();

                if (ubicacion.SiteRack != null)
                    ubicacion.SiteRack.Descripcion = ubicacion.SiteRack.Descripcion.ToUpper();

                if (ubicacion.Id == 0)
                    db.Ubicacion.AddObject(ubicacion);
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

        public Ubicacion ObtenerUbicacionUsuario(int idUbicacion)
        {
            Ubicacion result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.SingleOrDefault(w => w.Id == idUbicacion && w.Habilitado);
                if (result != null)
                {
                    db.LoadProperty(result, "Pais");
                    db.LoadProperty(result, "Campus");
                    db.LoadProperty(result, "Torre");
                    db.LoadProperty(result, "Piso");
                    db.LoadProperty(result, "Zona");
                    db.LoadProperty(result, "SubZona");
                    db.LoadProperty(result, "SiteRack");
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

        public string ObtenerDescripcionUbicacionUsuario(int idUsuario, bool ultimoNivel)
        {
            string result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Usuario usuario = db.Usuario.SingleOrDefault(w => w.Id == idUsuario && w.Habilitado);
                if (usuario != null)
                {
                    if (usuario.Ubicacion != null)
                    {
                        if (ultimoNivel)
                        {
                            if (usuario.Ubicacion.Pais != null)
                                result = usuario.Ubicacion.Pais.Descripcion;
                            if (usuario.Ubicacion.Campus != null)
                                result = usuario.Ubicacion.Campus.Descripcion;
                            if (usuario.Ubicacion.Torre != null)
                                result = usuario.Ubicacion.Torre.Descripcion;
                            if (usuario.Ubicacion.Piso != null)
                                result = usuario.Ubicacion.Piso.Descripcion;
                            if (usuario.Ubicacion.Zona != null)
                                result = usuario.Ubicacion.Zona.Descripcion;
                            if (usuario.Ubicacion.SubZona != null)
                                result = usuario.Ubicacion.SubZona.Descripcion;
                            if (usuario.Ubicacion.SiteRack != null)
                                result = usuario.Ubicacion.SiteRack.Descripcion;
                        }
                        else
                        {
                            if (usuario.Ubicacion.Pais != null)
                                result += usuario.Ubicacion.Pais.Descripcion;
                            if (usuario.Ubicacion.Campus != null)
                                result += ">" + usuario.Ubicacion.Campus.Descripcion;
                            if (usuario.Ubicacion.Torre != null)
                                result += ">" + usuario.Ubicacion.Torre.Descripcion;
                            if (usuario.Ubicacion.Piso != null)
                                result += ">" + usuario.Ubicacion.Piso.Descripcion;
                            if (usuario.Ubicacion.Zona != null)
                                result += ">" + usuario.Ubicacion.Zona.Descripcion;
                            if (usuario.Ubicacion.SubZona != null)
                                result += ">" + usuario.Ubicacion.SubZona.Descripcion;
                            if (usuario.Ubicacion.SiteRack != null)
                                result += ">" + usuario.Ubicacion.SiteRack.Descripcion;
                        }
                    }
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
    }
}
