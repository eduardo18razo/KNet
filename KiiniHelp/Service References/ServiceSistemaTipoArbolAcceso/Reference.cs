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

namespace KiiniHelp.ServiceSistemaTipoArbolAcceso {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceSistemaTipoArbolAcceso.IServiceTipoArbolAcceso")]
    public interface IServiceTipoArbolAcceso {
        
        [OperationContract(Action="http://tempuri.org/IServiceTipoArbolAcceso/ObtenerTiposArbolAcceso", ReplyAction="http://tempuri.org/IServiceTipoArbolAcceso/ObtenerTiposArbolAccesoResponse")]
        List<TipoArbolAcceso> ObtenerTiposArbolAcceso(bool insertarSeleccion);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTipoArbolAccesoChannel : IServiceTipoArbolAcceso, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTipoArbolAccesoClient : ClientBase<IServiceTipoArbolAcceso>, IServiceTipoArbolAcceso {
        
        public ServiceTipoArbolAccesoClient() {
        }
        
        public ServiceTipoArbolAccesoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTipoArbolAccesoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoArbolAccesoClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoArbolAccesoClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<TipoArbolAcceso> ObtenerTiposArbolAcceso(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposArbolAcceso(insertarSeleccion);
        }
    }
}
