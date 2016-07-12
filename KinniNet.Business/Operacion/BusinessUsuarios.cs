using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessUsuarios : IDisposable
    {
        private bool _proxy;
        public BusinessUsuarios(bool proxy = false)
        {
            _proxy = proxy;
        }

        public Usuario ObtenerUsuario(int idUsuario)
        {
            Usuario result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
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

        public void GuardarUsuario(Usuario usuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                usuario.ApellidoPaterno = usuario.ApellidoPaterno.ToUpper();
                usuario.ApellidoMaterno = usuario.ApellidoMaterno.ToUpper();
                usuario.Nombre = usuario.Nombre.ToUpper();
                //usuario.NombreUsuario = usuario.Nombre.Substring(0, 1).ToLower() + usuario.ApellidoPaterno.ToLower();
                usuario.NombreUsuario = usuario.Nombre.ToLower() + usuario.ApellidoPaterno.Substring(0, 1).ToLower();
                //usuario.Password = usuario.Nombre.Substring(0, 1).ToLower() + usuario.ApellidoPaterno.ToLower();
                usuario.Password = "1";
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                if (usuario.Id == 0)
                {
                    BusinessOrganizacion nOrganizacion = new BusinessOrganizacion();
                    BusinessUbicacion nUbicacion = new BusinessUbicacion();

                    usuario.Organizacion = nOrganizacion.ObtenerOrganizacion(usuario.Organizacion.IdHolding, usuario.Organizacion.IdCompania, usuario.Organizacion.IdDireccion, usuario.Organizacion.IdSubDireccion, usuario.Organizacion.IdGerencia, usuario.Organizacion.IdSubGerencia, usuario.Organizacion.IdJefatura);
                    usuario.Ubicacion = nUbicacion.ObtenerUbicacion(usuario.Ubicacion.IdPais, usuario.Ubicacion.IdCampus, usuario.Ubicacion.IdTorre, usuario.Ubicacion.IdPiso, usuario.Ubicacion.IdZona, usuario.Ubicacion.IdSubZona, usuario.Ubicacion.IdSiteRack);
                    db.Usuario.AddObject(usuario);
                    db.SaveChanges();
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
        }

        public List<Usuario> ObtenerUsuarios(int? idTipoUsuario)
        {
            List<Usuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Usuario> qry = db.Usuario;
                if (idTipoUsuario != null)
                    qry = qry.Where(w => w.IdTipoUsuario == idTipoUsuario);
                result = qry.ToList();
                foreach (Usuario usuario in result)
                {
                    db.LoadProperty(usuario, "TipoUsuario");
                    usuario.OrganizacionFinal = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, true);
                    usuario.OrganizacionCompleta = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, false);
                    usuario.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, true);
                    usuario.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, false);
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

        public List<Usuario> ObtenerUsuariosByGrupo(int idGrupo, int idNivel)
        {
            List<Usuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = (db.Usuario.Join(db.UsuarioGrupo, u => u.Id, ug => ug.IdUsuario, (u, ug) => new {u, ug})
                    .Where(@t => @t.ug.IdGrupoUsuario == idGrupo && @t.ug.SubGrupoUsuario.IdSubRol == idNivel)
                    .Select(@t => @t.u)).Distinct().ToList();
                //foreach (Usuario usuario in result)
                //{
                //    db.LoadProperty(usuario, "TipoUsuario");
                //    usuario.OrganizacionFinal = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, true);
                //    usuario.OrganizacionCompleta = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, false);
                //    usuario.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, true);
                //    usuario.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, false);
                //}

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

        public Usuario ObtenerDetalleUsuario(int idUsuario)
        {
            Usuario result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                db.LoadProperty(result, "CorreoUsuario");
                db.LoadProperty(result, "TelefonoUsuario");
                db.LoadProperty(result, "UsuarioRol");
                db.LoadProperty(result, "TipoUsuario");
                if (result != null)
                {
                    result.OrganizacionFinal = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(result.Id, true);
                    result.OrganizacionCompleta = new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(result.Id, false);
                    result.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(result.Id, true);
                    result.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(result.Id, false);
                    foreach (TelefonoUsuario telefono in result.TelefonoUsuario)
                    {
                        db.LoadProperty(telefono, "TipoTelefono");
                    }
                    foreach (var rol in result.UsuarioRol)
                    {
                        db.LoadProperty(rol, "RolTipoUsuario");
                        db.LoadProperty(rol.RolTipoUsuario, "Rol");
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

        public void Dispose()
        {

        }
    }
}
