using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceEncuesta : IServiceEncuesta
    {
        public List<Encuesta> ObtenerEncuestas(bool insertarSeleccion)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    return negocio.ObtenerEncuestas(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarEncuesta(Encuesta encuesta)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    negocio.GuardarEncuesta(encuesta);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
