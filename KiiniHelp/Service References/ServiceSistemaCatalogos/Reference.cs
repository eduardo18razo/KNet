﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceSistemaCatalogos {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSistemaCatalogos.IServiceCatalogos")]
    public interface IServiceCatalogos {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceCatalogos/ObtenerCatalogosMascaraCaptura", ReplyAction="http://tempuri.org/IServiceCatalogos/ObtenerCatalogosMascaraCapturaResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceCatalogosChannel : KiiniHelp.ServiceSistemaCatalogos.IServiceCatalogos, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceCatalogosClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceSistemaCatalogos.IServiceCatalogos>, KiiniHelp.ServiceSistemaCatalogos.IServiceCatalogos {
        
        public ServiceCatalogosClient() {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceCatalogosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion) {
            return base.Channel.ObtenerCatalogosMascaraCaptura(insertarSeleccion);
        }
    }
}
