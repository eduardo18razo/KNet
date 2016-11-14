using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Tickets;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceTicket
    {
        [OperationContract]
        Ticket CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura, bool campoRandom);

        [OperationContract]
        List<HelperTickets> ObtenerTicketsUsuario(int idUsuario, int pageIndex, int pageSize);

        [OperationContract]
        List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize);

        [OperationContract]
        void CambiarEstatus(int idTicket, int idEstatus, int idUsuario, string comentario);
        
        [OperationContract]
        void AutoAsignarTicket(int idTicket,  int idUsuario);

        [OperationContract]
        void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna, string comentario);

        [OperationContract]
        HelperDetalleTicket ObtenerDetalleTicket(int idTicket);

        [OperationContract]
        HelperDetalleTicket ObtenerDetalleTicketNoRegistrado(int idTicket, string cveRegistro);
    }

}
