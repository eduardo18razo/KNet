using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Services.Security.Interface;
using KinniNet.Core.Security;

namespace KiiniNet.Services.Security.Implementacion
{
    public class ServiceSecurity : IServiceSecurity
    {
        public bool Autenticate(string user, string password)
        {
            try
            {
                using (BusinessSecurity.Autenticacion negocio = new BusinessSecurity.Autenticacion())
                {
                    return negocio.Autenticate(user, password);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario GetUserDataAutenticate(string user, string password)
        {
            try
            {
                using (BusinessSecurity.Autenticacion negocio = new BusinessSecurity.Autenticacion())
                {
                    return negocio.GetUserDataAutenticate(user, password);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Menu> ObtenerMenuUsuario(int idUsuario, int idArea, bool arboles)
        {
            try
            {
                using (BusinessSecurity.Menus negocio = new BusinessSecurity.Menus())
                {
                    return negocio.ObtenerMenuUsuario(idUsuario, idArea, arboles);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
