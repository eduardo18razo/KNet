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
        Ticket CrearTicket(int idUsuario, int idUsuarioSolicito, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura, int idCanal, bool campoRandom, bool esTercero, bool esMail);

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

        [OperationContract]
        PreTicket GeneraPreticket(int idArbol, int idUsuarioSolicita, int idUsuarioLevanto, string observaciones);
    }

}
