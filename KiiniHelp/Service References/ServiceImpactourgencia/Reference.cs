﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceImpactourgencia {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceImpactourgencia.IServiceImpactoUrgencia")]
    public interface IServiceImpactoUrgencia {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceImpactoUrgencia/ObtenerPrioridad", ReplyAction="http://tempuri.org/IServiceImpactoUrgencia/ObtenerPrioridadResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Prioridad> ObtenerPrioridad(bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceImpactoUrgencia/ObtenerUrgencia", ReplyAction="http://tempuri.org/IServiceImpactoUrgencia/ObtenerUrgenciaResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Urgencia> ObtenerUrgencia(bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceImpactoUrgencia/ObtenerImpactoById", ReplyAction="http://tempuri.org/IServiceImpactoUrgencia/ObtenerImpactoByIdResponse")]
        KiiniNet.Entities.Cat.Sistema.Impacto ObtenerImpactoById(int idImpacto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceImpactoUrgencia/ObtenerImpactoByPrioridadUrgencia", ReplyAction="http://tempuri.org/IServiceImpactoUrgencia/ObtenerImpactoByPrioridadUrgenciaRespo" +
            "nse")]
        KiiniNet.Entities.Cat.Sistema.Impacto ObtenerImpactoByPrioridadUrgencia(int idPrioridad, int idUrgencia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceImpactoUrgencia/ObtenerAll", ReplyAction="http://tempuri.org/IServiceImpactoUrgencia/ObtenerAllResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Impacto> ObtenerAll(bool insertarSeleccion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceImpactoUrgenciaChannel : KiiniHelp.ServiceImpactourgencia.IServiceImpactoUrgencia, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceImpactoUrgenciaClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceImpactourgencia.IServiceImpactoUrgencia>, KiiniHelp.ServiceImpactourgencia.IServiceImpactoUrgencia {
        
        public ServiceImpactoUrgenciaClient() {
        }
        
        public ServiceImpactoUrgenciaClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceImpactoUrgenciaClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceImpactoUrgenciaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceImpactoUrgenciaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Prioridad> ObtenerPrioridad(bool insertarSeleccion) {
            return base.Channel.ObtenerPrioridad(insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Urgencia> ObtenerUrgencia(bool insertarSeleccion) {
            return base.Channel.ObtenerUrgencia(insertarSeleccion);
        }
        
        public KiiniNet.Entities.Cat.Sistema.Impacto ObtenerImpactoById(int idImpacto) {
            return base.Channel.ObtenerImpactoById(idImpacto);
        }
        
        public KiiniNet.Entities.Cat.Sistema.Impacto ObtenerImpactoByPrioridadUrgencia(int idPrioridad, int idUrgencia) {
            return base.Channel.ObtenerImpactoByPrioridadUrgencia(idPrioridad, idUrgencia);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Impacto> ObtenerAll(bool insertarSeleccion) {
            return base.Channel.ObtenerAll(insertarSeleccion);
        }
    }
}