﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceTicket {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceTicket.IServiceTicket")]
    public interface IServiceTicket {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTicket/Guardar", ReplyAction="http://tempuri.org/IServiceTicket/GuardarResponse")]
        void Guardar(int idUsuario, int idArbol, System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperCampoMascaraCaptura> lstCaptura);
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
        
        public void Guardar(int idUsuario, int idArbol, System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperCampoMascaraCaptura> lstCaptura) {
            base.Channel.Guardar(idUsuario, idArbol, lstCaptura);
        }
    }
}
