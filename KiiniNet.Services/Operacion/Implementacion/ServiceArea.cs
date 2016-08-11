using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public  class ServiceArea : IServiceArea
    {
        public List<Area> ObtenerAreasUsuario(int idUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuario(idUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasUsuarioPublico(bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuarioPublico(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasTipoUsuario(int idTipoUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasTipoUsuario(idTipoUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreas(bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreas(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Guardar(Area area)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    negocio.Guardar(area);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
