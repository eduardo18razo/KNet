using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServiceCanal : IServiceCanal
    {
        public List<Canal> ObtenerCanales(bool insertarSeleccion)
        {
            try
            {
                using (BusinessCanal negocio = new BusinessCanal())
                {
                    return negocio.ObtenerCanales(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
