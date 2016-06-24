using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessOrganizacion : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessOrganizacion(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<Holding> ObtenerHoldings(int idTipoUsuario, bool insertarSeleccion)
        {
            List<Holding> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Holding.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Holding { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Compania> ObtenerCompañias(int idTipoUsuario, int idHolding, bool insertarSeleccion)
        {
            List<Compania> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdHolding == idHolding).SelectMany(organizacion => db.Compañia.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdCompania && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Compania { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Direccion> ObtenerDirecciones(int idTipoUsuario, int idCompañia, bool insertarSeleccion)
        {
            List<Direccion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdCompania == idCompañia).SelectMany(organizacion => db.Direccion.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdDireccion && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Direccion { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<SubDireccion> ObtenerSubDirecciones(int idTipoUsuario, int idDireccoin, bool insertarSeleccion)
        {
            List<SubDireccion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdDireccion == idDireccoin).SelectMany(organizacion => db.SubDireccion.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdSubDireccion && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new SubDireccion { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Gerencia> ObtenerGerencias(int idTipoUsuario, int idSubdireccion, bool insertarSeleccion)
        {
            List<Gerencia> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdSubDireccion == idSubdireccion).SelectMany(organizacion => db.Gerencia.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdGerencia && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Gerencia { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<SubGerencia> ObtenerSubGerencias(int idTipoUsuario, int idGerencia, bool insertarSeleccion)
        {
            List<SubGerencia> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdGerencia == idGerencia).SelectMany(organizacion => db.SubGerencia.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdSubGerencia && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new SubGerencia { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Jefatura> ObtenerJefaturas(int idTipoUsuario, int idSubGerencia, bool insertarSeleccion)
        {
            List<Jefatura> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.Where(w => w.IdSubGerencia == idSubGerencia).SelectMany(organizacion => db.Jefatura.Where(w => w.IdTipoUsuario == idTipoUsuario && w.Id == organizacion.IdJefatura && w.Habilitado)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Jefatura { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public Organizacion ObtenerOrganizacion(int idHolding, int? idCompania, int? idDireccion, int? idSubDireccion, int? idGerencia, int? idSubGerencia, int? idJefatura)
        {
            Organizacion result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var qry = db.Organizacion.Where(w => w.IdHolding == idHolding);
                if (idCompania.HasValue)
                    qry = qry.Where(w => w.IdCompania == idCompania);
                else
                    qry = qry.Where(w => w.IdCompania == null);

                if (idDireccion.HasValue)
                    qry = qry.Where(w => w.IdDireccion == idDireccion);
                else
                    qry = qry.Where(w => w.IdDireccion == null);

                if (idSubDireccion.HasValue)
                    qry = qry.Where(w => w.IdSubDireccion == idSubDireccion);
                else
                    qry = qry.Where(w => w.IdSubDireccion == null);

                if (idGerencia.HasValue)
                    qry = qry.Where(w => w.IdGerencia == idGerencia);
                else
                    qry = qry.Where(w => w.IdGerencia == null);

                if (idSubGerencia.HasValue)
                    qry = qry.Where(w => w.IdSubGerencia == idSubGerencia);
                else
                    qry = qry.Where(w => w.IdSubGerencia == null);

                if (idJefatura.HasValue)
                    qry = qry.Where(w => w.IdJefatura == idJefatura);
                else
                    qry = qry.Where(w => w.IdJefatura == null);

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
        public void GuardarOrganizacion(Organizacion organizacion)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                organizacion.Habilitado = true;
                if (organizacion.Holding != null)
                    organizacion.Holding.Descripcion = organizacion.Holding.Descripcion.ToUpper();

                if (organizacion.Compania != null)
                    organizacion.Compania.Descripcion = organizacion.Compania.Descripcion.ToUpper();

                if (organizacion.Direccion != null)
                    organizacion.Direccion.Descripcion = organizacion.Direccion.Descripcion.ToUpper();

                if (organizacion.SubDireccion != null)
                    organizacion.SubDireccion.Descripcion = organizacion.SubDireccion.Descripcion.ToUpper();

                if (organizacion.Gerencia != null)
                    organizacion.Gerencia.Descripcion = organizacion.Gerencia.Descripcion.ToUpper();

                if (organizacion.SubGerencia != null)
                    organizacion.SubGerencia.Descripcion = organizacion.SubGerencia.Descripcion.ToUpper();

                if (organizacion.Jefatura != null)
                    organizacion.Jefatura.Descripcion = organizacion.Jefatura.Descripcion.ToUpper();

                if (organizacion.Id == 0)
                    db.Organizacion.AddObject(organizacion);
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
        public void GuardarHolding(Holding entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.Holding.AddObject(entidad);
                else
                {
                    Holding tmpHolding = db.Holding.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpHolding == null) return;
                    tmpHolding.Descripcion = entidad.Descripcion;
                    tmpHolding.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarCompania(Compania entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.Compañia.AddObject(entidad);
                else
                {
                    Compania tmpCompania = db.Compañia.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpCompania == null) return;
                    tmpCompania.Descripcion = entidad.Descripcion;
                    tmpCompania.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarDireccion(Direccion entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.Direccion.AddObject(entidad);
                else
                {
                    Direccion tmpDireccion = db.Direccion.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpDireccion == null) return;
                    tmpDireccion.Descripcion = entidad.Descripcion;
                    tmpDireccion.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarSubDireccion(SubDireccion entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.SubDireccion.AddObject(entidad);
                else
                {
                    SubDireccion tmpSubDireccion = db.SubDireccion.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpSubDireccion == null) return;
                    tmpSubDireccion.Descripcion = entidad.Descripcion;
                    tmpSubDireccion.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarGerencia(Gerencia entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.Gerencia.AddObject(entidad);
                else
                {
                    Gerencia tmpGerencia = db.Gerencia.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpGerencia == null) return;
                    tmpGerencia.Descripcion = entidad.Descripcion;
                    tmpGerencia.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarSubGerencia(SubGerencia entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.SubGerencia.AddObject(entidad);
                else
                {
                    SubGerencia tmpSubGerencia = db.SubGerencia.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpSubGerencia == null) return;
                    tmpSubGerencia.Descripcion = entidad.Descripcion;
                    tmpSubGerencia.Habilitado = entidad.Habilitado;
                }
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
        public void GuardarJefatura(Jefatura entidad)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                entidad.Descripcion = entidad.Descripcion.ToUpper();
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                entidad.Habilitado = true;
                if (entidad.Id == 0)
                    db.Jefatura.AddObject(entidad);
                else
                {
                    Jefatura tmpJefatura = db.Jefatura.SingleOrDefault(s => s.Id == entidad.Id);
                    if (tmpJefatura == null) return;
                    tmpJefatura.Descripcion = entidad.Descripcion;
                    tmpJefatura.Habilitado = entidad.Habilitado;
                }
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

        public Organizacion ObtenerOrganizacionUsuario(int idOrganizacion)
        {
            Organizacion result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Organizacion.SingleOrDefault(w => w.Id == idOrganizacion && w.Habilitado);
                if (result != null)
                {
                    db.LoadProperty(result, "Holding");
                    db.LoadProperty(result, "Compania");
                    db.LoadProperty(result, "Direccion");
                    db.LoadProperty(result, "SubDireccion");
                    db.LoadProperty(result, "Gerencia");
                    db.LoadProperty(result, "SubGerencia");
                    db.LoadProperty(result, "Jefatura");
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

        public string ObtenerDescripcionOrganizacionUsuario(int idUsuario, bool ultimoNivel)
        {
            string result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Usuario usuario = db.Usuario.SingleOrDefault(w => w.Id == idUsuario && w.Habilitado);
                if (usuario != null)
                {
                    if (usuario.Organizacion != null)
                    {
                        if (ultimoNivel)
                        {
                            if (usuario.Organizacion.Holding != null)
                                result = usuario.Organizacion.Holding.Descripcion;
                            if (usuario.Organizacion.Compania != null)
                                result = usuario.Organizacion.Compania.Descripcion;
                            if (usuario.Organizacion.Direccion != null)
                                result = usuario.Organizacion.Direccion.Descripcion;
                            if (usuario.Organizacion.SubDireccion != null)
                                result = usuario.Organizacion.SubDireccion.Descripcion;
                            if (usuario.Organizacion.Gerencia != null)
                                result = usuario.Organizacion.Gerencia.Descripcion;
                            if (usuario.Organizacion.SubGerencia != null)
                                result = usuario.Organizacion.SubGerencia.Descripcion;
                            if (usuario.Organizacion.Jefatura != null)
                                result = usuario.Organizacion.Jefatura.Descripcion;
                        }
                        else
                        {
                            if (usuario.Organizacion.Holding != null)
                                result += usuario.Organizacion.Holding.Descripcion;
                            if (usuario.Organizacion.Compania != null)
                                result += ">" + usuario.Organizacion.Compania.Descripcion;
                            if (usuario.Organizacion.Direccion != null)
                                result += ">" + usuario.Organizacion.Direccion.Descripcion;
                            if (usuario.Organizacion.SubDireccion != null)
                                result += ">" + usuario.Organizacion.SubDireccion.Descripcion;
                            if (usuario.Organizacion.Gerencia != null)
                                result += ">" + usuario.Organizacion.Gerencia.Descripcion;
                            if (usuario.Organizacion.SubGerencia != null)
                                result += ">" + usuario.Organizacion.SubGerencia.Descripcion;
                            if (usuario.Organizacion.Jefatura != null)
                                result += ">" + usuario.Organizacion.Jefatura.Descripcion;
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
