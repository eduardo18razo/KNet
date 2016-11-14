using System;
using System.Collections.Generic;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Tickets;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceTicket : IServiceTicket
    {
        public Ticket CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura, bool campoRandom)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    return negocio.CrearTicket(idUsuario, idArbol, lstCaptura, campoRandom);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperTickets> ObtenerTicketsUsuario(int idUsuario, int pageIndex, int pageSize)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    return negocio.ObtenerTicketsUsuario(idUsuario, pageIndex, pageSize);
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

        public void CambiarEstatus(int idTicket, int idEstatus, int idUsuario, string comentario)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.CambiarEstatus(idTicket, idEstatus, idUsuario, comentario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AutoAsignarTicket(int idTicket, int idUsuario)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.AutoAsignarTicket(idTicket, idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna, string comentario)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    negocio.CambiarAsignacionTicket(idTicket, idEstatusAsignacion, idUsuarioAsignado, idUsuarioAsigna, comentario);
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

        public HelperDetalleTicket ObtenerDetalleTicketNoRegistrado(int idTicket, string cveRegistro)
        {
            try
            {
                using (BusinessTicket negocio = new BusinessTicket())
                {
                    return negocio.ObtenerDetalleTicketNoRegistrado(idTicket, cveRegistro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
