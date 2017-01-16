using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;
using KinniNet.Data.Help;

namespace KinniNet.Core.Parametros
{
    public class BusinessParametros : IDisposable
    {
        private bool _proxy;
        public BusinessParametros(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        {

        }

        public List<ParametrosTelefonos> TelefonosObligatorios(int idTipoUsuario)
        {
            List<ParametrosTelefonos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ParametrosTelefonos.Where(w => w.IdTipoUsuario == idTipoUsuario).ToList();
                foreach (ParametrosTelefonos param in result)
                {
                    db.LoadProperty(param, "TipoTelefono");
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

        public List<TelefonoUsuario> ObtenerTelefonosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion)
        {
            List<TelefonoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = new List<TelefonoUsuario>();
                foreach (ParametrosTelefonos parametrosTelefonose in db.ParametrosTelefonos.Where(w => w.IdTipoUsuario == idTipoUsuario))
                {
                    db.LoadProperty(parametrosTelefonose, "TipoTelefono");
                    for (int i = 0; i < parametrosTelefonose.NumeroTelefonos; i++)
                    {
                        result.Add(new TelefonoUsuario { IdTipoTelefono = parametrosTelefonose.IdTipoTelefono, TipoTelefono = parametrosTelefonose.TipoTelefono });
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

        public List<CorreoUsuario> ObtenerCorreosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion)
        {
            List<CorreoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = new List<CorreoUsuario>();
                foreach (TipoUsuario tipoUsuario in db.TipoUsuario.Where(w => w.Id == idTipoUsuario))
                {
                    for (int i = 0; i < tipoUsuario.NumeroCorreos; i++)
                    {
                        result.Add(new CorreoUsuario());
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

        public ParametrosSla ObtenerParametrosSla()
        {
            ParametrosSla result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ParametrosSla.First();
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

        public ParametrosGenerales ObtenerParametrosGenerales()
        {
            ParametrosGenerales result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ParametrosGenerales.First();
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

        public List<AliasOrganizacion> ObtenerAliasOrganizacion(int idTipoUsuario)
        {
            List<AliasOrganizacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.AliasOrganizacion.Where(w => w.IdTipoUsuario == idTipoUsuario).ToList();
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

        public List<AliasUbicacion> ObtenerAliasUbicacion(int idTipoUsuario)
        {
            List<AliasUbicacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.AliasUbicacion.Where(w => w.IdTipoUsuario == idTipoUsuario).ToList();
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
}
