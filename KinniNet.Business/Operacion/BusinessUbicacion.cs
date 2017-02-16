using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new Pais { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Campus> ObtenerCampus(int idTipoUsuario, int idPais, bool insertarSeleccion)
        {
            List<Campus> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdPais == idPais).SelectMany(ubicacion => db.Campus.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdCampus && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new Campus { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Torre> ObtenerTorres(int idTipoUsuario, int idCampus, bool insertarSeleccion)
        {
            List<Torre> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdCampus == idCampus).SelectMany(ubicacion => db.Torre.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdTorre && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new Torre { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Piso> ObtenerPisos(int idTipoUsuario, int idTorre, bool insertarSeleccion)
        {
            List<Piso> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdTorre == idTorre).SelectMany(ubicacion => db.Piso.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdPiso && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new Piso { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Zona> ObtenerZonas(int idTipoUsuario, int idPiso, bool insertarSeleccion)
        {
            List<Zona> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdPiso == idPiso).SelectMany(ubicacion => db.Zona.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdZona && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new Zona { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<SubZona> ObtenerSubZonas(int idTipoUsuario, int idZona, bool insertarSeleccion)
        {
            List<SubZona> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdZona == idZona).SelectMany(ubicacion => db.SubZona.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdSubZona && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new SubZona { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<SiteRack> ObtenerSiteRacks(int idTipoUsuario, int idSubZona, bool insertarSeleccion)
        {
            List<SiteRack> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.Where(w => w.IdSubZona == idSubZona).SelectMany(ubicacion => db.SiteRack.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == ubicacion.IdSiteRack && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione, new SiteRack { Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public Ubicacion ObtenerUbicacion(int idPais, int? idCampus, int? idTorre, int? idPiso, int? idZona, int? idSubZona, int? idSiteRack)
        {
            Ubicacion result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
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
                throw new Exception("Error al Obtener Ubicacion");
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
                ubicacion.Habilitado = true;
                if (ubicacion.Campus != null)
                {
                    ubicacion.IdNivelUbicacion = 2;
                    ubicacion.Campus.Descripcion = ubicacion.Campus.Descripcion.ToUpper();
                    foreach (Domicilio domicilio in ubicacion.Campus.Domicilio)
                    {
                        domicilio.Calle = domicilio.Calle.ToUpper();
                        domicilio.NoExt = domicilio.NoExt.ToUpper();
                        domicilio.NoInt = domicilio.NoInt.ToUpper();
                    }
                    if (db.Campus.Join(db.Ubicacion, c => c.Id, u => u.IdCampus, (c, u) => new { c, u }).Any(@t => @t.u.IdPais == ubicacion.IdPais
                                                                                                                 && @t.c.Descripcion == ubicacion.Campus.Descripcion &&
                                                                                                                 @t.c.IdTipoUsuario == ubicacion.Campus.IdTipoUsuario))
                        throw new Exception("Esta Campus ya se encuetra registrado");
                }
                if (ubicacion.Torre != null)
                {
                    ubicacion.IdNivelUbicacion = 3;
                    ubicacion.Torre.Descripcion = ubicacion.Torre.Descripcion.ToUpper();
                    if (db.Torre.Join(db.Ubicacion, t => t.Id, u => u.IdTorre, (t, u) => new { t, u }).Any(@t1 => @t1.u.IdPais == ubicacion.IdPais && @t1.u.IdCampus == ubicacion.IdCampus
                                                                                                                && @t1.t.Descripcion == ubicacion.Torre.Descripcion &&
                                                                                                                @t1.t.IdTipoUsuario == ubicacion.Torre.IdTipoUsuario))
                        throw new Exception("Esta Torre ya se encuetra registrada");
                }

                if (ubicacion.Piso != null)
                {
                    ubicacion.IdNivelUbicacion = 4;
                    ubicacion.Piso.Descripcion = ubicacion.Piso.Descripcion.ToUpper();

                    if (db.Piso.Join(db.Ubicacion, p => p.Id, u => u.IdPiso, (p, u) => new { p, u }).Any(@t => @t.u.IdPais == ubicacion.IdPais && @t.u.IdCampus == ubicacion.IdCampus
                                                                                                             && @t.u.IdTorre == ubicacion.IdTorre
                                                                                                             && @t.p.Descripcion == ubicacion.Piso.Descripcion &&
                                                                                                             @t.p.IdTipoUsuario == ubicacion.Piso.IdTipoUsuario))
                        throw new Exception("Este Piso ya se encuetra registrado");
                }

                if (ubicacion.Zona != null)
                {
                    ubicacion.IdNivelUbicacion = 5;
                    ubicacion.Zona.Descripcion = ubicacion.Zona.Descripcion.ToUpper();

                    if (db.Zona.Join(db.Ubicacion, z => z.Id, u => u.IdZona, (z, u) => new { z, u }).Any(@t => @t.u.IdPais == ubicacion.IdPais && @t.u.IdCampus == ubicacion.IdCampus
                                                                                                             && @t.u.IdTorre == ubicacion.IdTorre
                                                                                                             && @t.u.IdPiso == ubicacion.IdPiso
                                                                                                             && @t.z.Descripcion == ubicacion.Zona.Descripcion &&
                                                                                                             @t.z.IdTipoUsuario == ubicacion.Zona.IdTipoUsuario))
                        throw new Exception("Esta Zona ya se encuetra registrada");
                }

                if (ubicacion.SubZona != null)
                {
                    ubicacion.IdNivelUbicacion = 6;
                    ubicacion.SubZona.Descripcion = ubicacion.SubZona.Descripcion.ToUpper();
                    if (db.SubZona.Join(db.Ubicacion, sz => sz.Id, u => u.IdSubZona, (sz, u) => new { sz, u }).Any(@t => @t.u.IdPais == ubicacion.IdPais && @t.u.IdCampus == ubicacion.IdCampus
                                                                                                                       && @t.u.IdTorre == ubicacion.IdTorre
                                                                                                                       && @t.u.IdPiso == ubicacion.IdPiso
                                                                                                                       && @t.u.IdZona == ubicacion.IdZona
                                                                                                                       && @t.sz.Descripcion == ubicacion.SubZona.Descripcion &&
                                                                                                                       @t.sz.IdTipoUsuario == ubicacion.SubZona.IdTipoUsuario))
                        throw new Exception("Esta SubZona ya se encuetra registrada");
                }

                if (ubicacion.SiteRack != null)
                {
                    ubicacion.IdNivelUbicacion = 7;
                    ubicacion.SiteRack.Descripcion = ubicacion.SiteRack.Descripcion.ToUpper();
                    if (db.SiteRack.Join(db.Ubicacion, sr => sr.Id, u => u.IdSiteRack, (sr, u) => new { sr, u })
                            .Any(@t => @t.u.IdPais == ubicacion.IdPais && @t.u.IdCampus == ubicacion.IdCampus
                                       && @t.u.IdTorre == ubicacion.IdTorre
                                       && @t.u.IdPiso == ubicacion.IdPiso
                                       && @t.u.IdZona == ubicacion.IdZona
                                       && @t.u.IdSubZona == ubicacion.IdSubZona
                                       && @t.sr.Descripcion == ubicacion.SiteRack.Descripcion &&
                                       @t.sr.IdTipoUsuario == ubicacion.SiteRack.IdTipoUsuario))
                        throw new Exception("Este SiteRack ya se encuetra registrado");
                }

                if (ubicacion.Id == 0)
                    db.Ubicacion.AddObject(ubicacion);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public List<Ubicacion> ObtenerUbicacionByRegionCode(string regionCode)
        {
            List<Ubicacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var qry = from u in db.Ubicacion
                          join d in db.Domicilio on u.IdCampus equals d.IdCampus
                          join col in db.Colonia on d.IdColonia equals col.Id
                          join m in db.Municipio on col.IdMunicipio equals m.Id
                          join e in db.Estado on m.IdEstado equals e.Id
                          where e.RegionCode == regionCode
                          select u;
                result = qry.Distinct().ToList();
                foreach (Ubicacion ubicacion in result)
                {
                    db.LoadProperty(ubicacion, "Pais");
                    db.LoadProperty(ubicacion, "Campus");
                    db.LoadProperty(ubicacion, "Torre");
                    db.LoadProperty(ubicacion, "Piso");
                    db.LoadProperty(ubicacion, "Zona");
                    db.LoadProperty(ubicacion, "SubZona");
                    db.LoadProperty(ubicacion, "SiteRack");
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
        public List<int> ObtenerUbicacionesByIdUbicacion(int idUbicacion)
        {
            List<int> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Ubicacion tmpUbicacion = db.Ubicacion.SingleOrDefault(s => s.Id == idUbicacion);
                IQueryable<Ubicacion> qry = db.Ubicacion.Where(w => w.Id != idUbicacion);
                if (tmpUbicacion != null)
                {
                    if (tmpUbicacion.IdSiteRack.HasValue)
                        qry = qry.Where(w => w.IdSiteRack == tmpUbicacion.IdSiteRack);
                    else if (tmpUbicacion.IdSubZona.HasValue)
                        qry = qry.Where(w => w.IdSubZona == tmpUbicacion.IdSubZona);
                    else if (tmpUbicacion.IdZona.HasValue)
                        qry = qry.Where(w => w.IdZona == tmpUbicacion.IdZona);
                    else if (tmpUbicacion.IdPiso.HasValue)
                        qry = qry.Where(w => w.IdPiso == tmpUbicacion.IdPiso);
                    else if (tmpUbicacion.IdTorre.HasValue)
                        qry = qry.Where(w => w.IdTorre == tmpUbicacion.IdTorre);
                    else if (tmpUbicacion.IdCampus.HasValue)
                        qry = qry.Where(w => w.IdCampus == tmpUbicacion.IdCampus);
                    else
                        qry = qry.Where(w => w.IdPais == tmpUbicacion.IdPais);
                }
                result = qry.Select(s => s.Id).ToList();
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
        public List<Ubicacion> ObtenerUbicaciones(int? idTipoUsuario, int? idPais, int? idCampus, int? idTorre, int? idPiso, int? idZona, int? idSubZona, int? idSiteRack)
        {
            List<Ubicacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Ubicacion> qry = db.Ubicacion;
                if (idTipoUsuario.HasValue)
                    qry = qry.Where(w => w.IdTipoUsuario == idTipoUsuario);

                if (idPais.HasValue)
                    qry = qry.Where(w => w.IdPais == idPais);

                if (idCampus.HasValue)
                    qry = qry.Where(w => w.IdCampus == idCampus);

                if (idTorre.HasValue)
                    qry = qry.Where(w => w.IdTorre == idTorre);

                if (idPiso.HasValue)
                    qry = qry.Where(w => w.IdPiso == idPiso);

                if (idZona.HasValue)
                    qry = qry.Where(w => w.IdZona == idZona);

                if (idSubZona.HasValue)
                    qry = qry.Where(w => w.IdSubZona == idSubZona);

                if (idSiteRack.HasValue)
                    qry = qry.Where(w => w.IdSiteRack == idSiteRack);
                qry = from q in qry
                      orderby q.Pais != null, q.Pais.Descripcion, q.Campus != null, q.Campus.Descripcion, q.Torre != null, q.Torre.Descripcion, q.Piso != null, q.Piso.Descripcion,
                      q.Zona != null, q.Zona.Descripcion, q.SubZona != null, q.SubZona.Descripcion, q.SiteRack != null, q.SiteRack.Descripcion ascending
                      select q;
                result = qry.ToList();
                foreach (Ubicacion ubicacion in result)
                {
                    db.LoadProperty(ubicacion, "TipoUsuario");
                    db.LoadProperty(ubicacion, "Pais");
                    db.LoadProperty(ubicacion, "Campus");
                    db.LoadProperty(ubicacion, "Torre");
                    db.LoadProperty(ubicacion, "Piso");
                    db.LoadProperty(ubicacion, "Zona");
                    db.LoadProperty(ubicacion, "SubZona");
                    db.LoadProperty(ubicacion, "SiteRack");
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
        public List<Ubicacion> BuscarPorPalabra(int? idTipoUsuario, int? idPais, int? idCampus, int? idTorre, int? idPiso, int? idZona, int? idSubZona, int? idSiteRack, string filtro)
        {
            {
                List<Ubicacion> result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    IQueryable<Ubicacion> qry = db.Ubicacion;
                    if (idTipoUsuario.HasValue)
                        qry = qry.Where(w => w.IdTipoUsuario == idTipoUsuario);

                    if (idPais.HasValue)
                        qry = qry.Where(w => w.IdPais == idPais);

                    if (idCampus.HasValue)
                        qry = qry.Where(w => w.IdCampus == idCampus);

                    if (idTorre.HasValue)
                        qry = qry.Where(w => w.IdTorre == idTorre);

                    if (idPiso.HasValue)
                        qry = qry.Where(w => w.IdPiso == idPiso);

                    if (idZona.HasValue)
                        qry = qry.Where(w => w.IdZona == idZona);

                    if (idSubZona.HasValue)
                        qry = qry.Where(w => w.IdSubZona == idSubZona);

                    if (idSiteRack.HasValue)
                        qry = qry.Where(w => w.IdSiteRack == idSiteRack);

                    if (filtro.Trim() != string.Empty)
                    {
                        filtro = filtro.ToUpper().Trim();
                        qry = qry.Where(w => w.Pais.Descripcion.Contains(filtro)
                            || w.Campus.Descripcion.Contains(filtro)
                            || w.Torre.Descripcion.Contains(filtro)
                            || w.Piso.Descripcion.Contains(filtro)
                            || w.Zona.Descripcion.Contains(filtro)
                            || w.SubZona.Descripcion.Contains(filtro)
                            || w.SiteRack.Descripcion.Contains(filtro));
                    }
                    qry = from q in qry
                          orderby q.Pais != null, q.Pais.Descripcion, q.Campus != null, q.Campus.Descripcion,
                              q.Torre != null, q.Torre.Descripcion, q.Piso != null, q.Piso.Descripcion,
                              q.Zona != null, q.Zona.Descripcion, q.SubZona != null, q.SubZona.Descripcion,
                              q.SiteRack != null, q.SiteRack.Descripcion ascending
                          select q;
                    result = qry.ToList();
                    foreach (Ubicacion ubicacion in result)
                    {
                        db.LoadProperty(ubicacion, "TipoUsuario");
                        db.LoadProperty(ubicacion, "Pais");
                        db.LoadProperty(ubicacion, "Campus");
                        db.LoadProperty(ubicacion, "Torre");
                        db.LoadProperty(ubicacion, "Piso");
                        db.LoadProperty(ubicacion, "Zona");
                        db.LoadProperty(ubicacion, "SubZona");
                        db.LoadProperty(ubicacion, "SiteRack");
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
        }
        public List<Ubicacion> ObtenerUbicacionesGrupos(List<int> grupos)
        {
            List<Ubicacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Ubicacion> qry = from u in db.Ubicacion
                                            join gu in db.GrupoUsuario on u.IdTipoUsuario equals gu.IdTipoUsuario
                                            where grupos.Contains(gu.Id)
                                            select u;

                result = qry.Distinct().ToList();
                foreach (Ubicacion ubicacion in result)
                {
                    db.LoadProperty(ubicacion, "Pais");
                    db.LoadProperty(ubicacion, "Campus");
                    db.LoadProperty(ubicacion, "Torre");
                    db.LoadProperty(ubicacion, "Piso");
                    db.LoadProperty(ubicacion, "Zona");
                    db.LoadProperty(ubicacion, "SubZona");
                    db.LoadProperty(ubicacion, "SiteRack");
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public string ObtenerDescripcionUbicacionById(int idUbicacion, bool ultimoNivel)
        {
            string result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Ubicacion ubicacion = db.Ubicacion.SingleOrDefault(w => w.Id == idUbicacion && w.Habilitado);
                if (ubicacion != null)
                {
                    if (ultimoNivel)
                    {
                        if (ubicacion.Pais != null)
                            result = ubicacion.Pais.Descripcion;
                        if (ubicacion.Campus != null)
                            result = ubicacion.Campus.Descripcion;
                        if (ubicacion.Torre != null)
                            result = ubicacion.Torre.Descripcion;
                        if (ubicacion.Piso != null)
                            result = ubicacion.Piso.Descripcion;
                        if (ubicacion.Zona != null)
                            result = ubicacion.Zona.Descripcion;
                        if (ubicacion.SubZona != null)
                            result = ubicacion.SubZona.Descripcion;
                        if (ubicacion.SiteRack != null)
                            result = ubicacion.SiteRack.Descripcion;
                    }
                    else
                    {
                        if (ubicacion.Pais != null)
                            result += ubicacion.Pais.Descripcion;
                        if (ubicacion.Campus != null)
                            result += ">" + ubicacion.Campus.Descripcion;
                        if (ubicacion.Torre != null)
                            result += ">" + ubicacion.Torre.Descripcion;
                        if (ubicacion.Piso != null)
                            result += ">" + ubicacion.Piso.Descripcion;
                        if (ubicacion.Zona != null)
                            result += ">" + ubicacion.Zona.Descripcion;
                        if (ubicacion.SubZona != null)
                            result += ">" + ubicacion.SubZona.Descripcion;
                        if (ubicacion.SiteRack != null)
                            result += ">" + ubicacion.SiteRack.Descripcion;
                    }
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
        public void ActualizarUbicacion(Ubicacion ub)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Ubicacion ubicacion = db.Ubicacion.SingleOrDefault(s => s.Id == ub.Id);
                if (ubicacion != null)
                {
                    if (ubicacion.Pais != null)
                        ubicacion.Pais.Descripcion = ub.Pais.Descripcion.ToUpper();

                    if (ubicacion.Campus != null)
                        ubicacion.Campus.Descripcion = ub.Campus.Descripcion.ToUpper();

                    if (ubicacion.Torre != null)
                        ubicacion.Torre.Descripcion = ub.Torre.Descripcion.ToUpper();

                    if (ubicacion.Piso != null)
                        ubicacion.Piso.Descripcion = ub.Piso.Descripcion.ToUpper();

                    if (ubicacion.Zona != null)
                        ubicacion.Zona.Descripcion = ub.Zona.Descripcion.ToUpper();

                    if (ubicacion.SubZona != null)
                        ubicacion.SubZona.Descripcion = ub.SubZona.Descripcion.ToUpper();

                    if (ubicacion.SiteRack != null)
                    {
                        if (db.SiteRack.Join(db.Ubicacion, sr => sr.Id, u => u.IdSiteRack, (sr, u) => new { sr, u })
                            .Any(@t => @t.u.IdPais == ubicacion.IdPais && @t.u.IdCampus == ubicacion.IdCampus
                                       && @t.u.IdTorre == ubicacion.IdTorre
                                       && @t.u.IdPiso == ubicacion.IdPiso
                                       && @t.u.IdZona == ubicacion.IdZona
                                       && @t.u.IdSubZona == ubicacion.IdSubZona
                                       && @t.sr.Descripcion == ubicacion.SiteRack.Descripcion &&
                                       @t.sr.IdTipoUsuario == ubicacion.SiteRack.IdTipoUsuario && t.sr.Id != ubicacion.Id))
                            throw new Exception("Este SiteRack ya se encuetra registrado");
                        ubicacion.SiteRack.Descripcion = ub.SiteRack.Descripcion.ToUpper();
                    }

                    if (ubicacion.Id == 0)
                        db.Ubicacion.AddObject(ubicacion);

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
        public Ubicacion ObtenerUbicacionById(int idUbicacion)
        {
            Ubicacion result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Ubicacion.SingleOrDefault(w => w.Id == idUbicacion);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public void HabilitarUbicacion(int idUbicacion, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                db.ContextOptions.ProxyCreationEnabled = true;
                Ubicacion ub = db.Ubicacion.SingleOrDefault(w => w.Id == idUbicacion);
                if (ub != null)
                {
                    if (ub.HitConsulta.Any() || ub.Ticket.Any() || ub.Usuario.Any())
                        throw new Exception("La ubicacion ya se encuetra con datos asociasdos no puede ser eliminada");
                    ub.Habilitado = habilitado;
                    var qry = db.Ubicacion.Where(w => w.IdTipoUsuario == ub.IdTipoUsuario && w.IdPais == ub.IdPais);
                    if (!habilitado)
                    {
                        qry = qry.Where(w => w.IdNivelUbicacion > ub.IdNivelUbicacion && w.Habilitado);
                        foreach (Ubicacion source in qry.OrderBy(o => o.Id))
                        {
                            if (ub.HitConsulta.Any() || ub.Ticket.Any() || ub.Usuario.Any())
                                throw new Exception("La ubicacion ya se encuetra con datos asociasdos no puede ser eliminada");
                            switch (ub.IdNivelUbicacion)
                            {
                                case 1:
                                    break;
                                case 2:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus)
                                    {
                                        source.Campus.Habilitado = false;
                                        if (source.IdTorre.HasValue)
                                            source.Torre.Habilitado = false;
                                        if (source.IdPiso.HasValue)
                                            source.Piso.Habilitado = false;
                                        if (source.IdZona.HasValue)
                                            source.Zona.Habilitado = false;
                                        if (source.IdSubZona.HasValue)
                                            source.SubZona.Habilitado = false;
                                        if (source.IdSiteRack.HasValue)
                                            source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                                case 3:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre)
                                    {
                                        source.Torre.Habilitado = false;
                                        if (source.IdPiso.HasValue)
                                            source.Piso.Habilitado = false;
                                        if (source.IdZona.HasValue)
                                            source.Zona.Habilitado = false;
                                        if (source.IdSubZona.HasValue)
                                            source.SubZona.Habilitado = false;
                                        if (source.IdSiteRack.HasValue)
                                            source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                                case 4:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso)
                                    {
                                        source.Piso.Habilitado = false;
                                        if (source.IdZona.HasValue)
                                            source.Zona.Habilitado = false;
                                        if (source.IdSubZona.HasValue)
                                            source.SubZona.Habilitado = false;
                                        if (source.IdSiteRack.HasValue)
                                            source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                                case 5:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona)
                                    {
                                        source.Zona.Habilitado = false;
                                        if (source.IdSubZona.HasValue)
                                            source.SubZona.Habilitado = false;
                                        if (source.IdSiteRack.HasValue)
                                            source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                                case 6:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona && source.IdSubZona == ub.IdSubZona)
                                    {
                                        source.SubZona.Habilitado = false;
                                        if (source.IdSiteRack.HasValue)
                                            source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                                case 7:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona && source.IdSubZona == ub.IdSubZona && source.IdSiteRack == ub.IdSiteRack)
                                    {
                                        source.SiteRack.Habilitado = false;
                                        source.Habilitado = false;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        qry = qry.Where(w => w.IdNivelUbicacion < ub.IdNivelUbicacion && !w.Habilitado);
                        switch (ub.IdNivelUbicacion)
                        {
                            case 1:
                                break;
                            case 2:
                                ub.Campus.Habilitado = true;
                                break;
                            case 3:
                                ub.Campus.Habilitado = true;
                                ub.Torre.Habilitado = true;
                                break;
                            case 4:
                                ub.Campus.Habilitado = true;
                                ub.Torre.Habilitado = true;
                                ub.Piso.Habilitado = true;
                                break;
                            case 5:
                                ub.Campus.Habilitado = true;
                                ub.Torre.Habilitado = true;
                                ub.Piso.Habilitado = true;
                                ub.Zona.Habilitado = true;
                                break;
                            case 6:
                                ub.Campus.Habilitado = true;
                                ub.Torre.Habilitado = true;
                                ub.Piso.Habilitado = true;
                                ub.Zona.Habilitado = true;
                                ub.SubZona.Habilitado = true;
                                break;
                            case 7:
                                ub.Campus.Habilitado = true;
                                ub.Torre.Habilitado = true;
                                ub.Piso.Habilitado = true;
                                ub.Zona.Habilitado = true;
                                ub.SubZona.Habilitado = true;
                                ub.SiteRack.Habilitado = true;
                                break;
                        }
                        if (ub.IdCampus.HasValue)
                            qry = qry.Where(w => w.IdCampus == ub.IdCampus);
                        foreach (Ubicacion source in qry.OrderBy(o => o.Id))
                        {
                            if (ub.HitConsulta.Any() || ub.Ticket.Any() || ub.Usuario.Any())
                                throw new Exception("La ubicacion ya se encuetra con datos asociasdos no puede ser eliminada");
                            switch (source.IdNivelUbicacion)
                            {
                                case 1:
                                    break;
                                case 2:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                                case 3:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Torre.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                                case 4:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Torre.Habilitado = true;
                                        source.Piso.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                                case 5:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Torre.Habilitado = true;
                                        source.Piso.Habilitado = true;
                                        source.Zona.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                                case 6:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona && source.IdSubZona == ub.IdSubZona)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Torre.Habilitado = true;
                                        source.Piso.Habilitado = true;
                                        source.Zona.Habilitado = true;
                                        source.SubZona.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                                case 7:
                                    if (source.IdPais == ub.IdPais && source.IdCampus == ub.IdCampus && source.IdTorre == ub.IdTorre && source.IdPiso == ub.IdPiso && source.IdZona == ub.IdZona && source.IdSubZona == ub.IdSubZona && source.IdSiteRack == ub.IdSiteRack)
                                    {
                                        source.Campus.Habilitado = true;
                                        source.Torre.Habilitado = true;
                                        source.Piso.Habilitado = true;
                                        source.Zona.Habilitado = true;
                                        source.SubZona.Habilitado = true;
                                        source.SiteRack.Habilitado = true;
                                        source.Habilitado = true;
                                    }
                                    break;
                            }
                        }
                    }
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
    }
}
