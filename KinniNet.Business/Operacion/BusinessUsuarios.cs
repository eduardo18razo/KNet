﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;
using KinniNet.Core.Demonio;
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
                db.ContextOptions.LazyLoadingEnabled = true;
                string tmpurl = usuario.Password;
                Guid g = Guid.NewGuid();
                ParametroCorreo correo = db.ParametroCorreo.SingleOrDefault(s => s.IdTipoCorreo == (int)BusinessVariables.EnumTipoCorreo.AltaUsuario && s.Habilitado);
                usuario.ApellidoPaterno = usuario.ApellidoPaterno.ToUpper();
                usuario.ApellidoMaterno = usuario.ApellidoMaterno.ToUpper();
                usuario.Nombre = usuario.Nombre.ToUpper();
                usuario.Password = ConfigurationManager.AppSettings["siteUrl"] + tmpurl + "?confirmacionalta=" +
                                   usuario.Id + "_" + g;
                usuario.UsuarioLinkPassword = new List<UsuarioLinkPassword>
                {
                    new UsuarioLinkPassword
                    {
                        Activo = true,
                        Link = g,
                        Fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                        IdTipoLink = (int) BusinessVariables.EnumTipoLink.Confirmacion
                    }
                };
                if (usuario.Id == 0)
                {
                    db.Usuario.AddObject(usuario);
                    db.SaveChanges();
                }
                usuario.Password = ConfigurationManager.AppSettings["siteUrl"] + tmpurl + "?confirmacionalta=" +
                                   usuario.Id + "_" + g;
                if (correo != null)
                {
                    String body = NamedFormat.Format(correo.Contenido, usuario);
                    foreach (CorreoUsuario correoUsuario in usuario.CorreoUsuario)
                    {
                        BusinessCorreo.SendMail(correoUsuario.Correo, correo.TipoCorreo.Descripcion, body);
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
                    List<int> correoEliminar = (from correoUsuario in userData.CorreoUsuario
                                                where !usuario.CorreoUsuario.Any(a => a.Correo == correoUsuario.Correo)
                                                select correoUsuario.Id).ToList();
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
                    List<int> telefonoEliminar = (from telefonoUsuario in userData.TelefonoUsuario
                                                  where
                                                      !usuario.TelefonoUsuario.Any(
                                                          a =>
                                                              a.Numero == telefonoUsuario.Numero &&
                                                              a.IdTipoTelefono == telefonoUsuario.IdTipoTelefono)
                                                  select telefonoUsuario.Id).ToList();
                    foreach (TelefonoUsuario telefonoUsuario in usuario.TelefonoUsuario)
                    {
                        if (
                            !db.TelefonoUsuario.Any(
                                a =>
                                    a.IdUsuario == idUsuario && a.Numero == telefonoUsuario.Numero &&
                                    a.IdTipoTelefono == telefonoUsuario.IdTipoTelefono))
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
                        rol.IdRolTipoUsuario =
                            db.RolTipoUsuario.Single(
                                s =>
                                    s.IdRol == rol.RolTipoUsuario.IdRol &&
                                    s.IdTipoUsuario == rol.RolTipoUsuario.IdTipoUsuario).Id;
                        rol.IdUsuario = idUsuario;
                        rol.RolTipoUsuario = null;
                    }

                    List<int> rolEliminar = (from usuarioRol in userData.UsuarioRol
                                             where
                                                 !usuario.UsuarioRol.Any(
                                                     a => a.IdUsuario == idUsuario && a.IdRolTipoUsuario == usuarioRol.IdRolTipoUsuario)
                                             select usuarioRol.Id).ToList();
                    foreach (UsuarioRol rol in usuario.UsuarioRol)
                    {
                        if (
                            !db.UsuarioRol.Any(
                                a => a.IdUsuario == idUsuario && a.IdRolTipoUsuario == rol.IdRolTipoUsuario))
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
                            if (
                                !usuario.UsuarioGrupo.Any(
                                    a =>
                                        a.IdUsuario == idUsuario && a.IdRol == ugpoDb.IdRol &&
                                        a.IdGrupoUsuario == ugpoDb.IdGrupoUsuario))
                                gruposEliminar.Add(ugpoDb.Id);
                        }
                        else
                        {
                            if (
                                !usuario.UsuarioGrupo.Any(
                                    a =>
                                        a.IdUsuario == idUsuario && a.IdRol == ugpoDb.IdRol &&
                                        a.IdGrupoUsuario == ugpoDb.IdGrupoUsuario &&
                                        a.IdSubGrupoUsuario == ugpoDb.IdSubGrupoUsuario))
                                gruposEliminar.Add(ugpoDb.Id);
                        }
                    }
                    foreach (UsuarioGrupo grupo in usuario.UsuarioGrupo)
                    {
                        if (grupo.IdSubGrupoUsuario == null)
                        {
                            if (
                                !db.UsuarioGrupo.Any(
                                    a =>
                                        a.IdUsuario == idUsuario && a.IdRol == grupo.IdRol &&
                                        a.IdGrupoUsuario == grupo.IdGrupoUsuario))
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
                            if (
                                !db.UsuarioGrupo.Any(
                                    a =>
                                        a.IdUsuario == idUsuario && a.IdRol == grupo.IdRol &&
                                        a.IdGrupoUsuario == grupo.IdGrupoUsuario &&
                                        a.IdSubGrupoUsuario == grupo.IdSubGrupoUsuario))
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
                    usuario.OrganizacionFinal =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, true);
                    usuario.OrganizacionCompleta =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, false);
                    usuario.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, true);
                    usuario.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id,
                        false);
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
                db.LoadProperty(result, "PreguntaReto");
                db.LoadProperty(result, "UsuarioRol");
                db.LoadProperty(result, "TipoUsuario");
                db.LoadProperty(result, "UsuarioGrupo");
                if (result != null)
                {
                    result.OrganizacionFinal =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(result.Id, true);
                    result.OrganizacionCompleta =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(result.Id, false);
                    result.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(result.Id, true);
                    result.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(result.Id,
                        false);
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

        public List<Usuario> ObtenerAtendedoresEncuesta(int idUsuario, List<int?> encuestas)
        {
            List<Usuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                bool supervisor = db.SubGrupoUsuario.Join(db.UsuarioGrupo, sgu => sgu.Id, ug => ug.IdSubGrupoUsuario,
                    (sgu, ug) => new { sgu, ug })
                    .Any(
                        @t =>
                            @t.sgu.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor &&
                            @t.ug.IdUsuario == idUsuario);
                var qry = from t in db.Ticket
                          join e in db.Encuesta on t.IdEncuesta equals e.Id
                          join tgu in db.TicketGrupoUsuario on t.Id equals tgu.IdTicket
                          join gu in db.GrupoUsuario on tgu.IdGrupoUsuario equals gu.Id
                          join u in db.Usuario on t.IdUsuarioResolvio equals u.Id
                          where gu.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención
                          select new { t, e, tgu, u };
                if (!supervisor)
                    qry = from q in qry
                          where q.t.IdUsuarioResolvio == idUsuario
                          select q;
                if (encuestas.Any())
                    qry = from q in qry
                          where encuestas.Contains(q.e.Id)
                          select q;
                result = qry.Select(s => s.u).Distinct().ToList();
                foreach (Usuario usuario in result)
                {
                    db.LoadProperty(usuario, "TipoUsuario");
                    usuario.OrganizacionFinal =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, true);
                    usuario.OrganizacionCompleta =
                        new BusinessOrganizacion().ObtenerDescripcionOrganizacionUsuario(usuario.Id, false);
                    usuario.UbicacionFinal = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id, true);
                    usuario.UbicacionCompleta = new BusinessUbicacion().ObtenerDescripcionUbicacionUsuario(usuario.Id,
                        false);
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

        public bool ValidaUserName(string nombreUsuario)
        {
            bool result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.Any(s => s.NombreUsuario == nombreUsuario);
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

        public bool ValidaConfirmacion(int idUsuario, string guid)
        {
            bool result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Guid guidParam = Guid.Parse(guid);
                result =
                    db.UsuarioLinkPassword.Any(
                        s =>
                            s.IdUsuario == idUsuario && s.Link == guidParam &&
                            s.IdTipoLink == (int)BusinessVariables.EnumTipoLink.Confirmacion && s.Activo);
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

        public string ValidaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo)
        {
            string result = string.Empty;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                TelefonoUsuario telefono = db.TelefonoUsuario.Single(s => s.Id == idTelefono);
                if (!db.SmsService.Any(a => a.IdUsuario == idUsuario && a.IdTipoLink == idTipoNotificacion && a.Numero == telefono.Numero && a.Mensaje == codigo && a.Enviado && a.Habilitado))
                {
                    throw new Exception(string.Format("Codigo incorrecto para Numero Telefonico {0}\n", telefono.Numero));
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
        public string TerminaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo)
        {
            string result = string.Empty;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                TelefonoUsuario telefono = db.TelefonoUsuario.Single(s => s.Id == idTelefono);
                List<SmsService> sms = db.SmsService.Where(a => a.IdUsuario == idUsuario && a.Habilitado).ToList();
                foreach (SmsService mensaje in sms)
                {
                    mensaje.Habilitado = false;
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
            return result;
        }

        public void EnviaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono)
        {
            try
            {
                Random generator = new Random();
                String codigo = generator.Next(0, 99999).ToString("D5");
                switch (idTipoNotificacion)
                {
                    case (int)BusinessVariables.EnumTipoLink.Confirmacion:
                        new BusinessDemonioSms().InsertarMensaje(idUsuario, idTipoNotificacion, idTelefono, codigo);
                        break;
                    case (int)BusinessVariables.EnumTipoLink.Reset:
                        new BusinessDemonioSms().InsertarMensaje(idUsuario, idTipoNotificacion, idTelefono, codigo);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
        }

        public void ActualizarTelefono(int idUsuario, int idTelefono, string numero)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                TelefonoUsuario telefono =
                    db.TelefonoUsuario.Single(
                        s =>
                            s.Id == idTelefono && s.IdUsuario == idUsuario &&
                            s.IdTipoTelefono == (int)BusinessVariables.EnumTipoTelefono.Celular);
                if (telefono != null)
                {
                    telefono.Numero = numero;
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

        public void ConfirmaCuenta(int idUsuario, string password, Dictionary<int, string> confirmaciones, List<PreguntaReto> pregunta, string link)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                db.ContextOptions.ProxyCreationEnabled = true;
                Usuario user = db.Usuario.Single(s => s.Id == idUsuario);
                if (user != null)
                {
                    Guid linkLlave = Guid.Parse(link);
                    user.UsuarioLinkPassword.Single(
                        s =>
                            s.IdTipoLink == (int)BusinessVariables.EnumTipoLink.Confirmacion &&
                            s.IdUsuario == idUsuario && s.Link == linkLlave).Activo = false;
                    user.PreguntaReto = new List<PreguntaReto>();
                    foreach (PreguntaReto reto in pregunta)
                    {
                        reto.Respuesta = SecurityUtils.CreateShaHash(reto.Respuesta);
                        db.PreguntaReto.AddObject(new PreguntaReto
                        {
                            IdUsuario = user.Id,
                            Pregunta = reto.Pregunta,
                            Respuesta = SecurityUtils.CreateShaHash(reto.Respuesta)
                        });
                    }
                    user.Password = SecurityUtils.CreateShaHash(password);
                    db.SaveChanges();
                    foreach (KeyValuePair<int, string> confirmacion in confirmaciones)
                    {
                        new BusinessDemonioSms().ConfirmaMensaje(user.Id,
                            (int)BusinessVariables.EnumTipoLink.Confirmacion, confirmacion.Key);
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
        }

        public string EnviaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, int idCorreo)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            string result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Random generator = new Random();
                String codigo = generator.Next(0, 99999).ToString("D5");
                Guid g = Guid.NewGuid();
                ParametroCorreo correo = db.ParametroCorreo.SingleOrDefault(s => s.IdTipoCorreo == (int)BusinessVariables.EnumTipoCorreo.RecuperarCuenta && s.Habilitado);
                if (correo != null)
                {

                    string to = db.CorreoUsuario.Single(s => s.Id == idCorreo).Correo;
                    db.LoadProperty(correo, "TipoCorreo");
                    Usuario usuario = db.Usuario.Single(u => u.Id == idUsuario);
                    db.LoadProperty(usuario, "CorreoUsuario");
                    String body = string.Format(correo.Contenido, usuario.NombreCompleto, ConfigurationManager.AppSettings["siteUrl"] + "/FrmRecuperar.aspx?confirmacionCodigo=" + BusinessQueryString.Encrypt(idUsuario + "_" + g) + "&correo=" + BusinessQueryString.Encrypt(idCorreo.ToString()) + "&code=" + BusinessQueryString.Encrypt(codigo), codigo);
                    BusinessCorreo.SendMail(to, correo.TipoCorreo.Descripcion, body);
                    usuario.UsuarioLinkPassword = new List<UsuarioLinkPassword>
                    {
                        new UsuarioLinkPassword
                        {
                            Activo = true,
                            Link = g,
                            Fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                            IdTipoLink = (int) BusinessVariables.EnumTipoLink.Reset,
                            Codigo = codigo
                        }
                    };
                    db.SaveChanges();
                    result = g.ToString();
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

        public void ValidaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                CorreoUsuario telefono = db.CorreoUsuario.Single(s => s.Id == idCorreo);
                Guid guidLink = Guid.Parse(link);
                if (!db.UsuarioLinkPassword.Any(a => a.IdUsuario == idUsuario && a.IdTipoLink == idTipoNotificacion && a.Link == guidLink && a.Codigo == codigo && a.Activo))
                {
                    throw new Exception(string.Format("Codigo incorrecto {0}\n", telefono.Correo));
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

        public void TerminaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                CorreoUsuario telefono = db.CorreoUsuario.Single(s => s.Id == idCorreo);
                Guid guidLink = Guid.Parse(link);
                List<UsuarioLinkPassword> links = db.UsuarioLinkPassword.Where(a => a.IdUsuario == idUsuario && a.Activo).ToList();
                foreach (UsuarioLinkPassword linkValue in links)
                {
                    linkValue.Activo = false;
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

        public Usuario BuscarUsuario(string usuario)
        {
            Usuario result = null;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                int idUsuario;
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                if (db.CorreoUsuario.Any(w => w.Correo == usuario))
                {
                    idUsuario = db.CorreoUsuario.First(w => w.Correo == usuario).IdUsuario;
                    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                }
                else if (db.TelefonoUsuario.Any(w => w.Numero == usuario && w.Obligatorio))
                {
                    idUsuario = db.TelefonoUsuario.First(w => w.Numero == usuario).IdUsuario;
                    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                }
                else if (db.Usuario.Any(w => w.NombreUsuario == usuario))
                {
                    idUsuario = db.Usuario.First(w => w.NombreUsuario == usuario).Id;
                    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                }
                db.LoadProperty(result, "PreguntaReto");
                db.LoadProperty(result, "TelefonoUsuario");
                db.LoadProperty(result, "CorreoUsuario");
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

        public void ValidaRespuestasReto(int idUsuario, Dictionary<int, string> preguntasReto)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                foreach (KeyValuePair<int, string> pregunta in preguntasReto)
                {
                    string respuesta = SecurityUtils.CreateShaHash(pregunta.Value);
                    if (!db.PreguntaReto.Any(w => w.IdUsuario == idUsuario && w.Id == pregunta.Key && w.Respuesta == respuesta))
                        throw new Exception("Verifique respuestas");
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

