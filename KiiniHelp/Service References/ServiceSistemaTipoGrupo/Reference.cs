﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceSistemaTipoGrupo {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSistemaTipoGrupo.IServiceTipoGrupo")]
    public interface IServiceTipoGrupo {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGrupo", ReplyAction="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGrupoResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGruposByRol", ReplyAction="http://tempuri.org/IServiceTipoGrupo/ObtenerTiposGruposByRolResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTipoGrupoChannel : KiiniHelp.ServiceSistemaTipoGrupo.IServiceTipoGrupo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTipoGrupoClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceSistemaTipoGrupo.IServiceTipoGrupo>, KiiniHelp.ServiceSistemaTipoGrupo.IServiceTipoGrupo {
        
        public ServiceTipoGrupoClient() {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoGrupoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTipoGrupoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoGrupo> ObtenerTiposGrupo(bool insertarSeleccion) {
            return base.Channel.ObtenerTiposGrupo(insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Sistema.TipoGrupo> ObtenerTiposGruposByRol(int idrol, bool insertarSeleccion) {
            return base.Channel.ObtenerTiposGruposByRol(idrol, insertarSeleccion);
        }
    }
}
