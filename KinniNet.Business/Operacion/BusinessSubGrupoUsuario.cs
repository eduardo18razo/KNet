using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessSubGrupoUsuario : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessSubGrupoUsuario(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<HelperSubGurpoUsuario> ObtenerSubGruposUsuario(int idGrupoUsuario, bool insertarSeleccion)
        {
            //TODO: REVISAR  METODO
            List<HelperSubGurpoUsuario> result = new List<HelperSubGurpoUsuario>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.SubGrupoUsuario.Where(w => w.IdGrupoUsuario == idGrupoUsuario).Select(s => new HelperSubGurpoUsuario { Id = s.Id, Descripcion = "CAMBIAR ESTA DESCRIPCION" }).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new HelperSubGurpoUsuario { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion });

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

        public SubGrupoUsuario ObtenerSubGrupoUsuario(int idGrupoUsuario, int idSubRol)
        {
            SubGrupoUsuario result = new SubGrupoUsuario();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.SubGrupoUsuario.SingleOrDefault(s => s.IdGrupoUsuario == idGrupoUsuario && s.IdSubRol == idSubRol);
                if (result != null)
                {
                    db.LoadProperty(result, "SubRol");
                    db.LoadProperty(result, "GrupoUsuario");
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
    }
}
