﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceSistemaSubRol {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSistemaSubRol.IServiceSubRol")]
    public interface IServiceSubRol {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSubRol/ObtenerSubRolesByTipoGrupo", ReplyAction="http://tempuri.org/IServiceSubRol/ObtenerSubRolesByTipoGrupoResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerSubRolesByTipoGrupo(int idTipoGrupo, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSubRol/ObtenerSubRolById", ReplyAction="http://tempuri.org/IServiceSubRol/ObtenerSubRolByIdResponse")]
        KiiniNet.Entities.Cat.Sistema.SubRol ObtenerSubRolById(int idSubRol);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSubRol/ObtenerSubRolesByGrupoUsuarioRol", ReplyAction="http://tempuri.org/IServiceSubRol/ObtenerSubRolesByGrupoUsuarioRolResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerSubRolesByGrupoUsuarioRol(int idGrupoUsuario, int idRol, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSubRol/ObtenerTipoSubRol", ReplyAction="http://tempuri.org/IServiceSubRol/ObtenerTipoSubRolResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerTipoSubRol(int idTipoGrupo, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceSubRol/ObtenerEscalacion", ReplyAction="http://tempuri.org/IServiceSubRol/ObtenerEscalacionResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Parametros.SubRolEscalacionPermitida> ObtenerEscalacion(int idSubRol, int idEstatusAsignacion, System.Nullable<int> nivelActual);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceSubRolChannel : KiiniHelp.ServiceSistemaSubRol.IServiceSubRol, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSubRolClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceSistemaSubRol.IServiceSubRol>, KiiniHelp.ServiceSistemaSubRol.IServiceSubRol {
        
        public ServiceSubRolClient() {
        }
        
        public ServiceSubRolClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSubRolClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSubRolClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSubRolClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerSubRolesByTipoGrupo(int idTipoGrupo, bool insertarSeleccion) {
            return base.Channel.ObtenerSubRolesByTipoGrupo(idTipoGrupo, insertarSeleccion);
        }
        
        public KiiniNet.Entities.Cat.Sistema.SubRol ObtenerSubRolById(int idSubRol) {
            return base.Channel.ObtenerSubRolById(idSubRol);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerSubRolesByGrupoUsuarioRol(int idGrupoUsuario, int idRol, bool insertarSeleccion) {
            return base.Channel.ObtenerSubRolesByGrupoUsuarioRol(idGrupoUsuario, idRol, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.SubRol> ObtenerTipoSubRol(int idTipoGrupo, bool insertarSeleccion) {
            return base.Channel.ObtenerTipoSubRol(idTipoGrupo, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Parametros.SubRolEscalacionPermitida> ObtenerEscalacion(int idSubRol, int idEstatusAsignacion, System.Nullable<int> nivelActual) {
            return base.Channel.ObtenerEscalacion(idSubRol, idEstatusAsignacion, nivelActual);
        }
    }
}
