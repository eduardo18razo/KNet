﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceParametrosSistema {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceParametrosSistema.IServiceParametros")]
    public interface IServiceParametros {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParametros/TelefonosObligatorios", ReplyAction="http://tempuri.org/IServiceParametros/TelefonosObligatoriosResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Parametros.ParametrosTelefonos> TelefonosObligatorios(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParametros/ObtenerTelefonosParametrosIdTipoUsuario", ReplyAction="http://tempuri.org/IServiceParametros/ObtenerTelefonosParametrosIdTipoUsuarioResp" +
            "onse")]
        System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.TelefonoUsuario> ObtenerTelefonosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParametros/ObtenerCorreosParametrosIdTipoUsuario", ReplyAction="http://tempuri.org/IServiceParametros/ObtenerCorreosParametrosIdTipoUsuarioRespon" +
            "se")]
        System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.CorreoUsuario> ObtenerCorreosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParametros/ObtenerParametrosGenerales", ReplyAction="http://tempuri.org/IServiceParametros/ObtenerParametrosGeneralesResponse")]
        KiiniNet.Entities.Parametros.ParametrosGenerales ObtenerParametrosGenerales();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceParametrosChannel : KiiniHelp.ServiceParametrosSistema.IServiceParametros, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceParametrosClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceParametrosSistema.IServiceParametros>, KiiniHelp.ServiceParametrosSistema.IServiceParametros {
        
        public ServiceParametrosClient() {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParametrosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParametrosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Parametros.ParametrosTelefonos> TelefonosObligatorios(int idTipoUsuario) {
            return base.Channel.TelefonosObligatorios(idTipoUsuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.TelefonoUsuario> ObtenerTelefonosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerTelefonosParametrosIdTipoUsuario(idTipoUsuario, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.CorreoUsuario> ObtenerCorreosParametrosIdTipoUsuario(int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerCorreosParametrosIdTipoUsuario(idTipoUsuario, insertarSeleccion);
        }
        
        public KiiniNet.Entities.Parametros.ParametrosGenerales ObtenerParametrosGenerales() {
            return base.Channel.ObtenerParametrosGenerales();
        }
    }
}
