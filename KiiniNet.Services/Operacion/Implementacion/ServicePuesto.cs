using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServicePuesto : IServicePuesto
    {
        public List<Puesto> ObtenerPuestos(bool insertarSeleccion)
        {
            try
            {
                using (BusinessPuesto negocio = new BusinessPuesto())
                {
                    return negocio.ObtenerPuestos(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Guardar(Puesto puesto)
        {
            try
            {
                using (BusinessPuesto negocio = new BusinessPuesto())
                {
                    negocio.Guardar(puesto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
