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
                    //BusinessOrganizacion nOrganizacion = new BusinessOrganizacion();
                    //BusinessUbicacion nUbicacion = new BusinessUbicacion();

                    //usuario.Organizacion = nOrganizacion.ObtenerOrganizacion(usuario.Organizacion.IdHolding, usuario.Organizacion.IdCompania, usuario.Organizacion.IdDireccion, usuario.Organizacion.IdSubDireccion, usuario.Organizacion.IdGerencia, usuario.Organizacion.IdSubGerencia, usuario.Organizacion.IdJefatura);
                    //usuario.Ubicacion = nUbicacion.ObtenerUbicacion(usuario.Ubicacion.IdPais, usuario.Ubicacion.IdCampus, usuario.Ubicacion.IdTorre, usuario.Ubicacion.IdPiso, usuario.Ubicacion.IdZona, usuario.Ubicacion.IdSubZona, usuario.Ubicacion.IdSiteRack);
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

        public void ActualizarUsuario(int idUsuario, Usuario usuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Usuario userData = db.Usuario.SingleOrDefault(u => u.Id == idUsuario);
                db.ContextOptions.LazyLoadingEnabled = true;
                if (userData != null)
                {
                    userData.ApellidoMaterno = usuario.ApellidoMaterno.Trim().ToUpper();
                    userData.ApellidoPaterno = usuario.ApellidoPaterno.Trim().ToUpper();
                    userData.Nombre = usuario.Nombre.Trim().ToUpper();
                    userData.IdPuesto = usuario.IdPuesto;
                    userData.IdOrganizacion = usuario.IdOrganizacion;
                    userData.IdUbicacion = usuario.IdUbicacion;
                    List<int> correoEliminar = (from correoUsuario in userData.CorreoUsuario where !usuario.CorreoUsuario.Any(a => a.Correo == correoUsuario.Correo) select correoUsuario.Id).ToList();
                    foreach (CorreoUsuario correoUsuario in usuario.CorreoUsuario)
                    {
                        if (!db.CorreoUsuario.Any(a => a.IdUsuario == idUsuario && a.Correo == correoUsuario.Correo))
                            userData.CorreoUsuario.Add(new CorreoUsuario
                            {
                                IdUsuario = idUsuario,
                                Correo = correoUsuario.Correo
                            });
                    }
                    foreach (int i in correoEliminar)
                    {
                        db.CorreoUsuario.DeleteObject(db.CorreoUsuario.SingleOrDefault(w => w.Id == i));
                    }
                    List<int> telefonoEliminar = (from telefonoUsuario in userData.TelefonoUsuario where !usuario.TelefonoUsuario.Any(a => a.Numero == telefonoUsuario.Numero && a.IdTipoTelefono == telefonoUsuario.IdTipoTelefono) select telefonoUsuario.Id).ToList();
                    foreach (TelefonoUsuario telefonoUsuario in usuario.TelefonoUsuario)
                    {
                        if (!db.TelefonoUsuario.Any(a => a.IdUsuario == idUsuario && a.Numero == telefonoUsuario.Numero && a.IdTipoTelefono == telefonoUsuario.IdTipoTelefono))
                            userData.TelefonoUsuario.Add(new TelefonoUsuario
                            {
                                IdUsuario = idUsuario,
                                Numero = telefonoUsuario.Numero,
                                IdTipoTelefono = telefonoUsuario.IdTipoTelefono,
                                Extension = telefonoUsuario.Extension
                            });
                    }
                    foreach (int i in telefonoEliminar)
                    {
                        db.TelefonoUsuario.DeleteObject(db.TelefonoUsuario.SingleOrDefault(w => w.Id == i));
                    }

                    foreach (UsuarioRol rol in usuario.UsuarioRol)
                    {
                        rol.IdRolTipoUsuario = db.RolTipoUsuario.Single(s => s.IdRol == rol.RolTipoUsuario.IdRol && s.IdTipoUsuario == rol.RolTipoUsuario.IdTipoUsuario).Id;
                        rol.IdUsuario = idUsuario;
                        rol.RolTipoUsuario = null;
                    }

                    List<int> rolEliminar = (from usuarioRol in userData.UsuarioRol where !usuario.UsuarioRol.Any(a => a.IdUsuario == idUsuario && a.IdRolTipoUsuario == usuarioRol.IdRolTipoUsuario) select usuarioRol.Id).ToList();
                    foreach (UsuarioRol rol in usuario.UsuarioRol)
                    {
                        if (!db.UsuarioRol.Any(a => a.IdUsuario == idUsuario && a.IdRolTipoUsuario == rol.IdRolTipoUsuario))
                            userData.UsuarioRol.Add(new UsuarioRol
                            {
                                IdUsuario = idUsuario,
                                IdRolTipoUsuario = rol.IdRolTipoUsuario
                            });
                    }

                    foreach (int i in rolEliminar)
                    {
                        db.UsuarioRol.DeleteObject(db.UsuarioRol.SingleOrDefault(w => w.Id == i));
                    }

                    List<int> gruposEliminar = new List<int>();
                    foreach (UsuarioGrupo ugpoDb in userData.UsuarioGrupo)
                    {
                        if (ugpoDb.IdSubGrupoUsuario == null)
                        {
                            if (!usuario.UsuarioGrupo.Any(a =>a.IdUsuario == idUsuario && a.IdRol == ugpoDb.IdRol && a.IdGrupoUsuario == ugpoDb.IdGrupoUsuario))
                                gruposEliminar.Add(ugpoDb.Id);
                        }
                        else
                        {
                            if (!usuario.UsuarioGrupo.Any(a => a.IdUsuario == idUsuario && a.IdRol == ugpoDb.IdRol && a.IdGrupoUsuario == ugpoDb.IdGrupoUsuario && a.IdSubGrupoUsuario == ugpoDb.IdSubGrupoUsuario))
                                gruposEliminar.Add(ugpoDb.Id);
                        }
                    }
                    foreach (UsuarioGrupo grupo in usuario.UsuarioGrupo)
                    {
                        if (grupo.IdSubGrupoUsuario == null)
                        {
                            if (!db.UsuarioGrupo.Any(a => a.IdUsuario == idUsuario && a.IdRol == grupo.IdRol && a.IdGrupoUsuario == grupo.IdGrupoUsuario))
                                userData.UsuarioGrupo.Add(new UsuarioGrupo
                                {
                                    IdUsuario = idUsuario,
                                    IdRol = grupo.IdRol,
                                    IdGrupoUsuario = grupo.IdGrupoUsuario,
                                    IdSubGrupoUsuario = grupo.IdSubGrupoUsuario
                                });
                        }
                        else
                        {
                            if (!db.UsuarioGrupo.Any(a => a.IdUsuario == idUsuario && a.IdRol == grupo.IdRol && a.IdGrupoUsuario == grupo.IdGrupoUsuario && a.IdSubGrupoUsuario == grupo.IdSubGrupoUsuario))
                                userData.UsuarioGrupo.Add(new UsuarioGrupo
                                {
                                    IdUsuario = idUsuario,
                                    IdRol = grupo.IdRol,
                                    IdGrupoUsuario = grupo.IdGrupoUsuario,
                                    IdSubGrupoUsuario = grupo.IdSubGrupoUsuario
                                });
                        }


                    }

                    foreach (int i in gruposEliminar)
                    {
                        db.UsuarioGrupo.DeleteObject(db.UsuarioGrupo.SingleOrDefault(w => w.Id == i));
                    }

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
                result = (db.Usuario.Join(db.UsuarioGrupo, u => u.Id, ug => ug.IdUsuario, (u, ug) => new { u, ug })
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
                db.LoadProperty(result, "UsuarioGrupo");
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
                    foreach (UsuarioGrupo grupo in result.UsuarioGrupo)
                    {
                        db.LoadProperty(grupo, "GrupoUsuario");
                        db.LoadProperty(grupo, "SubGrupoUsuario");
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
