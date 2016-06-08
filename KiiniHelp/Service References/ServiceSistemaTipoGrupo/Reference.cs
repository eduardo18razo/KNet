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

namespace KiiniHelp.ServiceSistemaTipoGrupo {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceSistemaTipoGrupo.IServiceTipoGrupo")]
    public interface IServiceTipoGrupo {
        
        [OperationContract(Action="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGrupo", ReplyAction="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGrupoResponse")]
        List<TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion);
        
        [OperationContract(Action="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGruposByRol", ReplyAction="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGruposByRolResponse")]
        List<TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTipoGrupoChannel : IServiceTipoGrupo, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTipoGrupoClient : ClientBase<IServiceTipoGrupo>, IServiceTipoGrupo {
        
        public ServiceTipoGrupoClient() {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoGrupoClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposGrupo(insertarSeleccion);
        }
        
        public List<TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion) {
            return base.Channel.ObtenerTiposGruposByRol(idrol, insertarSeleccion);
        }
    }
}
