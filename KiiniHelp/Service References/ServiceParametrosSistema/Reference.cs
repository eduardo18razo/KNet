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
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;

namespace KiiniHelp.ServiceParametrosSistema {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceParametrosSistema.IServiceParametros")]
    public interface IServiceParametros {
        
        [OperationContract(Action="http://tempuri.org/IServiceParametros/TelefonosObligatorios", ReplyAction="http://tempuri.org/IServiceParametros/TelefonosObligatoriosResponse")]
        List<ParametrosTelefonos> TelefonosObligatorios(int idTipoUsuario);
        
        [OperationContract(Action="http://tempuri.org/IServiceParametros/ObtenerTelefonosParametrosIdTipoUsuario", ReplyAction="http://tempuri.org/IServiceParametros/ObtenerTelefonosParametrosIdTipoUsuarioResp" +
            "onse")]
        List<TelefonoUsuario> ObtenerTelefonosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion);
        
        [OperationContract(Action="http://tempuri.org/IServiceParametros/ObtenerCorreosParametrosIdTipoUsuario", ReplyAction="http://tempuri.org/IServiceParametros/ObtenerCorreosParametrosIdTipoUsuarioRespon" +
            "se")]
        List<CorreoUsuario> ObtenerCorreosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IServiceParametrosChannel : IServiceParametros, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceParametrosClient : ClientBase<IServiceParametros>, IServiceParametros {
        
        public ServiceParametrosClient() {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParametrosClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<ParametrosTelefonos> TelefonosObligatorios(int idTipoUsuario) {
            return base.Channel.TelefonosObligatorios(idTipoUsuario);
        }
        
        public List<TelefonoUsuario> ObtenerTelefonosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerTelefonosParametrosIdTipoUsuario(idTipoUsuario, insertarSeleccion);
        }
        
        public List<CorreoUsuario> ObtenerCorreosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerCorreosParametrosIdTipoUsuario(idTipoUsuario, insertarSeleccion);
        }
    }
}
