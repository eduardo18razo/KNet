﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceUsuarios : IServiceUsuarios
    {
        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.GuardarUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> ObtenerUsuarios(int? idTipoUsuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ObtenerUsuarios(idTipoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerDetalleUsuario(int idUsuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ObtenerDetalleUsuario(idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> ObtenerUsuariosByGrupo(int idGrupo, int idNivel)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ObtenerUsuariosByGrupo(idGrupo, idNivel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarUsuario(int idUsuario, Usuario usuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.ActualizarUsuario(idUsuario, usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> ObtenerAtendedoresEncuesta(int idUsuario, List<int?> encuestas)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ObtenerAtendedoresEncuesta(idUsuario, encuestas);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidaUserName(string nombreUsuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ValidaUserName(nombreUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidaConfirmacion(int idUsuario, string guid)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ValidaConfirmacion(idUsuario, guid);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ValidaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.ValidaCodigoVerificacionSms(idUsuario, idTipoNotificacion, idTelefono, codigo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnviaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.EnviaCodigoVerificacionSms(idUsuario, idTipoNotificacion, idTelefono);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarTelefono(int idUsuario, int idTelefono, string numero)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.ActualizarTelefono(idUsuario, idTelefono, numero);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConfirmaCuenta(int idUsuario, string password, Dictionary<int, string> confirmaciones, List<PreguntaReto> pregunta, string link)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.ConfirmaCuenta(idUsuario, password, confirmaciones, pregunta, link);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario BuscarUsuario(string usuario)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.BuscarUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EnviaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, int idCorreo)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    return negocio.EnviaCodigoVerificacionCorreo(idUsuario, idTipoNotificacion, idCorreo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ValidaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.ValidaCodigoVerificacionCorreo(idUsuario, idTipoNotificacion,link, idCorreo, codigo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ValidaRespuestasReto(int idUsuario, Dictionary<int, string> preguntasReto)
        {
            try
            {
                using (BusinessUsuarios negocio = new BusinessUsuarios())
                {
                    negocio.ValidaRespuestasReto(idUsuario, preguntasReto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
