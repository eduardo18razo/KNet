﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceFrecuencia {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceFrecuencia.IServiceFrecuencia")]
    public interface IServiceFrecuencia {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenGeneral", ReplyAction="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenGeneralResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenGeneral(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenConsulta", ReplyAction="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenConsultaResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenConsulta(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenServicio", ReplyAction="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenServicioResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenServicio(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenIncidente", ReplyAction="http://tempuri.org/IServiceFrecuencia/ObtenerTopTenIncidenteResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenIncidente(int idTipoUsuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceFrecuenciaChannel : KiiniHelp.ServiceFrecuencia.IServiceFrecuencia, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceFrecuenciaClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceFrecuencia.IServiceFrecuencia>, KiiniHelp.ServiceFrecuencia.IServiceFrecuencia {
        
        public ServiceFrecuenciaClient() {
        }
        
        public ServiceFrecuenciaClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceFrecuenciaClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceFrecuenciaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceFrecuenciaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenGeneral(int idTipoUsuario) {
            return base.Channel.ObtenerTopTenGeneral(idTipoUsuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenConsulta(int idTipoUsuario) {
            return base.Channel.ObtenerTopTenConsulta(idTipoUsuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenServicio(int idTipoUsuario) {
            return base.Channel.ObtenerTopTenServicio(idTipoUsuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Helper.HelperFrecuencia> ObtenerTopTenIncidente(int idTipoUsuario) {
            return base.Channel.ObtenerTopTenIncidente(idTipoUsuario);
        }
    }
}