using System;
using System.Collections.Generic;
using KiiniNet.Entities.Helper;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceTicket : IServiceTicket
    {
        public void CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.CrearTicket(idUsuario, idArbol, lstCaptura);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    return negocio.ObtenerTickets(idUsuario, pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CambiarEstatus(int idTicket, int idEstatus, int idUsuario)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.CambiarEstatus(idTicket, idEstatus, idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AutoAsignarTicket(int idTicket,  int idUsuario)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.AutoAsignarTicket(idTicket,  idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.CambiarAsignacionTicket(idTicket, idEstatusAsignacion, idUsuarioAsignado, idUsuarioAsigna);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public HelperDetalleTicket ObtenerDetalleTicket(int idTicket)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    return negocio.ObtenerDetalleTicket(idTicket);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
