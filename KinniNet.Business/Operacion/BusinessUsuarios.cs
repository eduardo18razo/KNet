using System;
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

        public void GuardarUsuario(Usuario usuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                usuario.ApellidoPaterno = usuario.ApellidoPaterno.ToUpper();
                usuario.ApellidoMaterno = usuario.ApellidoMaterno.ToUpper();
                usuario.Nombre = usuario.Nombre.ToUpper();
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

        public void Dispose()
        {

        }

        
    }
}
