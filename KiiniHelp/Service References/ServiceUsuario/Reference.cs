﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceUsuario {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceUsuario.IServiceUsuarios")]
    public interface IServiceUsuarios {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceUsuarios/GuardarUsuario", ReplyAction="http://tempuri.org/IServiceUsuarios/GuardarUsuarioResponse")]
        void GuardarUsuario(KiiniNet.Entities.Operacion.Usuarios.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceUsuarios/ObtenerUsuarios", ReplyAction="http://tempuri.org/IServiceUsuarios/ObtenerUsuariosResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.Usuario> ObtenerUsuarios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceUsuarios/ObtenerDetalleUsuario", ReplyAction="http://tempuri.org/IServiceUsuarios/ObtenerDetalleUsuarioResponse")]
        KiiniNet.Entities.Operacion.Usuarios.Usuario ObtenerDetalleUsuario(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceUsuarios/ObtenerUsuariosByGrupo", ReplyAction="http://tempuri.org/IServiceUsuarios/ObtenerUsuariosByGrupoResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.Usuario> ObtenerUsuariosByGrupo(int idGrupo, int idNivel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceUsuariosChannel : KiiniHelp.ServiceUsuario.IServiceUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceUsuariosClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceUsuario.IServiceUsuarios>, KiiniHelp.ServiceUsuario.IServiceUsuarios {
        
        public ServiceUsuariosClient() {
        }
        
        public ServiceUsuariosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceUsuariosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceUsuariosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceUsuariosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void GuardarUsuario(KiiniNet.Entities.Operacion.Usuarios.Usuario usuario) {
            base.Channel.GuardarUsuario(usuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.Usuario> ObtenerUsuarios() {
            return base.Channel.ObtenerUsuarios();
        }
        
        public KiiniNet.Entities.Operacion.Usuarios.Usuario ObtenerDetalleUsuario(int idUsuario) {
            return base.Channel.ObtenerDetalleUsuario(idUsuario);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Operacion.Usuarios.Usuario> ObtenerUsuariosByGrupo(int idGrupo, int idNivel) {
            return base.Channel.ObtenerUsuariosByGrupo(idGrupo, idNivel);
        }
    }
}
