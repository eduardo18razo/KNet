﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceSeguridad {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSeguridad.IServiceSecurity")]
    public interface IServiceSecurity {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/Autenticate", ReplyAction="http://tempuri.org/IServiceSecurity/AutenticateResponse")]
        bool Autenticate(string user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/GetUserDataAutenticate", ReplyAction="http://tempuri.org/IServiceSecurity/GetUserDataAutenticateResponse")]
        KiiniNet.Entities.Operacion.Usuarios.Usuario GetUserDataAutenticate(string user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/GetUserInvitadoDataAutenticate", ReplyAction="http://tempuri.org/IServiceSecurity/GetUserInvitadoDataAutenticateResponse")]
        KiiniNet.Entities.Operacion.Usuarios.Usuario GetUserInvitadoDataAutenticate(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/ChangePassword", ReplyAction="http://tempuri.org/IServiceSecurity/ChangePasswordResponse")]
        void ChangePassword(int idUsuario, string contrasenaActual, string contrasenaNueva);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/ObtenerMenuUsuario", ReplyAction="http://tempuri.org/IServiceSecurity/ObtenerMenuUsuarioResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Menu> ObtenerMenuUsuario(int idUsuario, int idArea, bool arboles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/ObtenerMenuPublico", ReplyAction="http://tempuri.org/IServiceSecurity/ObtenerMenuPublicoResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Menu> ObtenerMenuPublico(int idTipoUsuario, int idArea, bool arboles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSecurity/RecuperarCuenta", ReplyAction="http://tempuri.org/IServiceSecurity/RecuperarCuentaResponse")]
        void RecuperarCuenta(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo, string contrasena, string tipoRecuperacion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceSecurityChannel : KiiniHelp.ServiceSeguridad.IServiceSecurity, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSecurityClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceSeguridad.IServiceSecurity>, KiiniHelp.ServiceSeguridad.IServiceSecurity {
        
        public ServiceSecurityClient() {
        }
        
        public ServiceSecurityClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSecurityClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSecurityClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSecurityClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Autenticate(string user, string password) {
            return base.Channel.Autenticate(user, password);
        }
        
        public KiiniNet.Entities.Operacion.Usuarios.Usuario GetUserDataAutenticate(string user, string password) {
            return base.Channel.GetUserDataAutenticate(user, password);
        }
        
        public KiiniNet.Entities.Operacion.Usuarios.Usuario GetUserInvitadoDataAutenticate(int idTipoUsuario) {
            return base.Channel.GetUserInvitadoDataAutenticate(idTipoUsuario);
        }
        
        public void ChangePassword(int idUsuario, string contrasenaActual, string contrasenaNueva) {
            base.Channel.ChangePassword(idUsuario, contrasenaActual, contrasenaNueva);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Menu> ObtenerMenuUsuario(int idUsuario, int idArea, bool arboles) {
            return base.Channel.ObtenerMenuUsuario(idUsuario, idArea, arboles);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.Menu> ObtenerMenuPublico(int idTipoUsuario, int idArea, bool arboles) {
            return base.Channel.ObtenerMenuPublico(idTipoUsuario, idArea, arboles);
        }
        
        public void RecuperarCuenta(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo, string contrasena, string tipoRecuperacion) {
            base.Channel.RecuperarCuenta(idUsuario, idTipoNotificacion, link, idCorreo, codigo, contrasena, tipoRecuperacion);
        }
    }
}
