using System;
using System.Collections.Generic;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceUsuarios" en el código y en el archivo de configuración a la vez.
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
    }
}
