﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceSistemaTipoUsuario {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSistemaTipoUsuario.IServiceTipoUsuario")]
    public interface IServiceTipoUsuario {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuarioResidentes", ReplyAction="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuarioResidentesResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuarioResidentes(bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuarioInvitados", ReplyAction="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuarioInvitadosResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuarioInvitados(bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuario", ReplyAction="http://tempuri.org/IServiceTipoUsuario/ObtenerTiposUsuarioResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuario(bool insertarSeleccion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTipoUsuarioChannel : KiiniHelp.ServiceSistemaTipoUsuario.IServiceTipoUsuario, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTipoUsuarioClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceSistemaTipoUsuario.IServiceTipoUsuario>, KiiniHelp.ServiceSistemaTipoUsuario.IServiceTipoUsuario {
        
        public ServiceTipoUsuarioClient() {
        }
        
        public ServiceTipoUsuarioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTipoUsuarioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoUsuarioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoUsuarioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuarioResidentes(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposUsuarioResidentes(insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuarioInvitados(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposUsuarioInvitados(insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoUsuario> ObtenerTiposUsuario(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposUsuario(insertarSeleccion);
        }
    }
}
