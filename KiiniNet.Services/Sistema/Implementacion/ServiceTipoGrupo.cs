using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServiceTipoGrupo : IServiceTipoGrupo
    {
        public List<TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion)
        {
            try
            {
                using (BusinessTipoGrupo negocio = new BusinessTipoGrupo())
                {
                    return negocio.ObtenerTiposGrupo(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion)
        {
            try
            {
                using (BusinessTipoGrupo negocio = new BusinessTipoGrupo())
                {
                    return negocio.ObtenerTiposGruposByRol(idrol, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
