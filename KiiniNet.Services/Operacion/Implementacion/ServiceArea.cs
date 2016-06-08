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
        public List<Area> ObtenerAreasUsuario(int idUsuario)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuario(idUsuario);
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
    }
}
