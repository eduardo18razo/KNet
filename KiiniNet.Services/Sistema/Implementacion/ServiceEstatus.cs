using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServiceEstatus : IServiceEstatus
    {
        public List<EstatusTicket> ObtenerEstatusTicket(bool insertarSeleccion)
        {
            try
            {
                using (BusinessEstatus negocio = new BusinessEstatus())
                {
                    return negocio.ObtenerEstatusTicket(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusAsignacion> ObtenerEstatusAsignacion(bool insertarSeleccion)
        {
            try
            {
                using (BusinessEstatus negocio = new BusinessEstatus())
                {
                    return negocio.ObtenerEstatusAsignacion(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusTicket> ObtenerEstatusTicketUsuario(int idUsuario, bool esPropietario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessEstatus negocio = new BusinessEstatus())
                {
                    return negocio.ObtenerEstatusTicketUsuario(idUsuario, esPropietario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusAsignacion> ObtenerEstatusAsignacionUsuario(int idUsuario, int idSubRol, int estatusAsignacionActual, bool esPropietario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessEstatus negocio = new BusinessEstatus())
                {
                    return negocio.ObtenerEstatusAsignacionUsuario(idUsuario,  idSubRol,  estatusAsignacionActual,  esPropietario,  insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
