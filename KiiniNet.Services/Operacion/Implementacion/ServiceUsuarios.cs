﻿using System.Collections.Generic;
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
            using (BusinessUsuarios negocio = new BusinessUsuarios())
            {
                negocio.GuardarUsuario(usuario);
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            using (BusinessUsuarios negocio = new BusinessUsuarios())
            {
                return negocio.ObtenerUsuarios();
            }
        }

        public Usuario ObtenerDetalleUsuario(int idUsuario)
        {
            using (BusinessUsuarios negocio = new BusinessUsuarios())
            {
               return negocio.ObtenerDetalleUsuario(idUsuario);
            }
        }
    }
}
