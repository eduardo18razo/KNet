﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceTicket {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceTicket.IServiceTicket")]
    public interface IServiceTicket {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/CrearTicket", ReplyAction="http://tempuri.org/IServiceTicket/CrearTicketResponse")]
        KiiniNet.Entities.Operacion.Tickets.Ticket CrearTicket(int idUsuario, int idUsuarioSolicito, int idArbol, System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperCampoMascaraCaptura> lstCaptura, int idCanal, bool campoRandom, bool esTercero);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/ObtenerTicketsUsuario", ReplyAction="http://tempuri.org/IServiceTicket/ObtenerTicketsUsuarioResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperTickets> ObtenerTicketsUsuario(int idUsuario, int pageIndex, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/ObtenerTickets", ReplyAction="http://tempuri.org/IServiceTicket/ObtenerTicketsResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/CambiarEstatus", ReplyAction="http://tempuri.org/IServiceTicket/CambiarEstatusResponse")]
        void CambiarEstatus(int idTicket, int idEstatus, int idUsuario, string comentario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/AutoAsignarTicket", ReplyAction="http://tempuri.org/IServiceTicket/AutoAsignarTicketResponse")]
        void AutoAsignarTicket(int idTicket, int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/CambiarAsignacionTicket", ReplyAction="http://tempuri.org/IServiceTicket/CambiarAsignacionTicketResponse")]
        void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna, string comentario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/ObtenerDetalleTicket", ReplyAction="http://tempuri.org/IServiceTicket/ObtenerDetalleTicketResponse")]
        KiiniNet.Entities.Helper.HelperDetalleTicket ObtenerDetalleTicket(int idTicket);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/ObtenerDetalleTicketNoRegistrado", ReplyAction="http://tempuri.org/IServiceTicket/ObtenerDetalleTicketNoRegistradoResponse")]
        KiiniNet.Entities.Helper.HelperDetalleTicket ObtenerDetalleTicketNoRegistrado(int idTicket, string cveRegistro);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTicketChannel : KiiniHelp.ServiceTicket.IServiceTicket, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTicketClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceTicket.IServiceTicket>, KiiniHelp.ServiceTicket.IServiceTicket {
        
        public ServiceTicketClient() {
        }
        
        public ServiceTicketClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTicketClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTicketClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTicketClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public KiiniNet.Entities.Operacion.Tickets.Ticket CrearTicket(int idUsuario, int idUsuarioSolicito, int idArbol, System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperCampoMascaraCaptura> lstCaptura, int idCanal, bool campoRandom, bool esTercero) {
            return base.Channel.CrearTicket(idUsuario, idUsuarioSolicito, idArbol, lstCaptura, idCanal, campoRandom, esTercero);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperTickets> ObtenerTicketsUsuario(int idUsuario, int pageIndex, int pageSize) {
            return base.Channel.ObtenerTicketsUsuario(idUsuario, pageIndex, pageSize);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize) {
            return base.Channel.ObtenerTickets(idUsuario, pageIndex, pageSize);
        }
        
        public void CambiarEstatus(int idTicket, int idEstatus, int idUsuario, string comentario) {
            base.Channel.CambiarEstatus(idTicket, idEstatus, idUsuario, comentario);
        }
        
        public void AutoAsignarTicket(int idTicket, int idUsuario) {
            base.Channel.AutoAsignarTicket(idTicket, idUsuario);
        }
        
        public void CambiarAsignacionTicket(int idTicket, int idEstatusAsignacion, int idUsuarioAsignado, int idUsuarioAsigna, string comentario) {
            base.Channel.CambiarAsignacionTicket(idTicket, idEstatusAsignacion, idUsuarioAsignado, idUsuarioAsigna, comentario);
        }
        
        public KiiniNet.Entities.Helper.HelperDetalleTicket ObtenerDetalleTicket(int idTicket) {
            return base.Channel.ObtenerDetalleTicket(idTicket);
        }
        
        public KiiniNet.Entities.Helper.HelperDetalleTicket ObtenerDetalleTicketNoRegistrado(int idTicket, string cveRegistro) {
            return base.Channel.ObtenerDetalleTicketNoRegistrado(idTicket, cveRegistro);
        }
    }
}
