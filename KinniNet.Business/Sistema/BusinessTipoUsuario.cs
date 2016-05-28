using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessTipoUsuario : IDisposable
    {
        private bool _proxy;
        public BusinessTipoUsuario(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<TipoUsuario> ObtenerTiposUsuarioResidentes(bool insertarSeleccion)
        {
            List<TipoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoUsuario.Where(w => w.Habilitado && w.EsResidente).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new TipoUsuario { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public List<TipoUsuario> ObtenerTiposUsuarioInvitados(bool insertarSeleccion)
        {
            List<TipoUsuario> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoUsuario.Where(w => w.Habilitado && !w.EsResidente).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new TipoUsuario { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
