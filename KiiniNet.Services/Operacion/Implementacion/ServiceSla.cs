using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceSla : IServiceSla
    {
        public List<SLA> ObtenerSla(bool insertarSeleccion)
        {
            try
            {
                using (BusinessSla negocio = new BusinessSla())
                {
                    return negocio.ObtenerSla(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Guardar(SLA sla)
        {
            try
            {
                using (BusinessSla negocio = new BusinessSla())
                {
                    negocio.Guardar(sla);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
