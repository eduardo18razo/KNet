﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniHelp.ServiceSistemaCatalogos {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceSistemaCatalogos.IServiceCatalogos")]
    public interface IServiceCatalogos {
        
        [OperationContract(Action="http://tempuri.org/IServiceCatalogos/ObtenerCatalogosMascaraCaptura", ReplyAction="http://tempuri.org/IServiceCatalogos/ObtenerCatalogosMascaraCapturaResponse")]
        List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IServiceCatalogosChannel : IServiceCatalogos, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceCatalogosClient : ClientBase<IServiceCatalogos>, IServiceCatalogos {
        
        public ServiceCatalogosClient() {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceCatalogosClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceCatalogosClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion) {
            return base.Channel.ObtenerCatalogosMascaraCaptura(insertarSeleccion);
        }
    }
}
