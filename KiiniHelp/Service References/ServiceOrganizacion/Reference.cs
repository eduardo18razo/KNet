﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceOrganizacion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceOrganizacion.IServiceOrganizacion")]
    public interface IServiceOrganizacion {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerHoldings", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerHoldingsResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Holding> ObtenerHoldings(int idTipoUsuario, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerCompañias", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerCompañiasResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Compania> ObtenerCompañias(int idTipoUsuario, int idHolding, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerDirecciones", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerDireccionesResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Direccion> ObtenerDirecciones(int idTipoUsuario, int idCompañia, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerSubDirecciones", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerSubDireccionesResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.SubDireccion> ObtenerSubDirecciones(int idTipoUsuario, int idDireccoin, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerGerencias", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerGerenciasResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Gerencia> ObtenerGerencias(int idTipoUsuario, int idSubdireccion, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerSubGerencias", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerSubGerenciasResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.SubGerencia> ObtenerSubGerencias(int idTipoUsuario, int idGerencia, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerJefaturas", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerJefaturasResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Jefatura> ObtenerJefaturas(int idTipoUsuario, int idSubGerencia, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacion", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionResponse")]
        KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacion(int idHolding, System.Nullable<int> idCompania, System.Nullable<int> idDireccion, System.Nullable<int> idSubDireccion, System.Nullable<int> idGerencia, System.Nullable<int> idSubGerencia, System.Nullable<int> idJefatura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarOrganizacion", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarOrganizacionResponse")]
        void GuardarOrganizacion(KiiniNet.Entities.Cat.Operacion.Organizacion organizacion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarHolding", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarHoldingResponse")]
        void GuardarHolding(KiiniNet.Entities.Cat.Arbol.Organizacion.Holding entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarCompania", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarCompaniaResponse")]
        void GuardarCompania(KiiniNet.Entities.Cat.Arbol.Organizacion.Compania entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarDireccion", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarDireccionResponse")]
        void GuardarDireccion(KiiniNet.Entities.Cat.Arbol.Organizacion.Direccion entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarSubDireccion", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarSubDireccionResponse")]
        void GuardarSubDireccion(KiiniNet.Entities.Cat.Arbol.Organizacion.SubDireccion entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarGerencia", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarGerenciaResponse")]
        void GuardarGerencia(KiiniNet.Entities.Cat.Arbol.Organizacion.Gerencia entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarSubGerencia", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarSubGerenciaResponse")]
        void GuardarSubGerencia(KiiniNet.Entities.Cat.Arbol.Organizacion.SubGerencia entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/GuardarJefatura", ReplyAction="http://tempuri.org/IServiceOrganizacion/GuardarJefaturaResponse")]
        void GuardarJefatura(KiiniNet.Entities.Cat.Arbol.Organizacion.Jefatura entidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionUsuario", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionUsuarioResponse")]
        KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacionUsuario(int idOrganizacion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizaciones", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionesResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.Organizacion> ObtenerOrganizaciones(System.Nullable<int> idTipoUsuario, System.Nullable<int> idHolding, System.Nullable<int> idCompania, System.Nullable<int> idDireccion, System.Nullable<int> idSubDireccion, System.Nullable<int> idGerencia, System.Nullable<int> idSubGerencia, System.Nullable<int> idJefatura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/HabilitarOrganizacion", ReplyAction="http://tempuri.org/IServiceOrganizacion/HabilitarOrganizacionResponse")]
        void HabilitarOrganizacion(int idOrganizacion, bool habilitado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionById", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionByIdResponse")]
        KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacionById(int idOrganizacion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ActualizarOrganizacion", ReplyAction="http://tempuri.org/IServiceOrganizacion/ActualizarOrganizacionResponse")]
        void ActualizarOrganizacion(KiiniNet.Entities.Cat.Operacion.Organizacion org);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionesGrupos", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionesGruposResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.Organizacion> ObtenerOrganizacionesGrupos(System.Collections.Generic.List<int> grupos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionesByIdOrganizacion", ReplyAction="http://tempuri.org/IServiceOrganizacion/ObtenerOrganizacionesByIdOrganizacionResp" +
            "onse")]
        System.Collections.Generic.List<int> ObtenerOrganizacionesByIdOrganizacion(int idUbicacion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceOrganizacionChannel : KiiniHelp.ServiceOrganizacion.IServiceOrganizacion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceOrganizacionClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceOrganizacion.IServiceOrganizacion>, KiiniHelp.ServiceOrganizacion.IServiceOrganizacion {
        
        public ServiceOrganizacionClient() {
        }
        
        public ServiceOrganizacionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceOrganizacionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOrganizacionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOrganizacionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Holding> ObtenerHoldings(int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerHoldings(idTipoUsuario, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Compania> ObtenerCompañias(int idTipoUsuario, int idHolding, bool insertarSeleccion) {
            return base.Channel.ObtenerCompañias(idTipoUsuario, idHolding, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Direccion> ObtenerDirecciones(int idTipoUsuario, int idCompañia, bool insertarSeleccion) {
            return base.Channel.ObtenerDirecciones(idTipoUsuario, idCompañia, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.SubDireccion> ObtenerSubDirecciones(int idTipoUsuario, int idDireccoin, bool insertarSeleccion) {
            return base.Channel.ObtenerSubDirecciones(idTipoUsuario, idDireccoin, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Gerencia> ObtenerGerencias(int idTipoUsuario, int idSubdireccion, bool insertarSeleccion) {
            return base.Channel.ObtenerGerencias(idTipoUsuario, idSubdireccion, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.SubGerencia> ObtenerSubGerencias(int idTipoUsuario, int idGerencia, bool insertarSeleccion) {
            return base.Channel.ObtenerSubGerencias(idTipoUsuario, idGerencia, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Organizacion.Jefatura> ObtenerJefaturas(int idTipoUsuario, int idSubGerencia, bool insertarSeleccion) {
            return base.Channel.ObtenerJefaturas(idTipoUsuario, idSubGerencia, insertarSeleccion);
        }
        
        public KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacion(int idHolding, System.Nullable<int> idCompania, System.Nullable<int> idDireccion, System.Nullable<int> idSubDireccion, System.Nullable<int> idGerencia, System.Nullable<int> idSubGerencia, System.Nullable<int> idJefatura) {
            return base.Channel.ObtenerOrganizacion(idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
        }
        
        public void GuardarOrganizacion(KiiniNet.Entities.Cat.Operacion.Organizacion organizacion) {
            base.Channel.GuardarOrganizacion(organizacion);
        }
        
        public void GuardarHolding(KiiniNet.Entities.Cat.Arbol.Organizacion.Holding entidad) {
            base.Channel.GuardarHolding(entidad);
        }
        
        public void GuardarCompania(KiiniNet.Entities.Cat.Arbol.Organizacion.Compania entidad) {
            base.Channel.GuardarCompania(entidad);
        }
        
        public void GuardarDireccion(KiiniNet.Entities.Cat.Arbol.Organizacion.Direccion entidad) {
            base.Channel.GuardarDireccion(entidad);
        }
        
        public void GuardarSubDireccion(KiiniNet.Entities.Cat.Arbol.Organizacion.SubDireccion entidad) {
            base.Channel.GuardarSubDireccion(entidad);
        }
        
        public void GuardarGerencia(KiiniNet.Entities.Cat.Arbol.Organizacion.Gerencia entidad) {
            base.Channel.GuardarGerencia(entidad);
        }
        
        public void GuardarSubGerencia(KiiniNet.Entities.Cat.Arbol.Organizacion.SubGerencia entidad) {
            base.Channel.GuardarSubGerencia(entidad);
        }
        
        public void GuardarJefatura(KiiniNet.Entities.Cat.Arbol.Organizacion.Jefatura entidad) {
            base.Channel.GuardarJefatura(entidad);
        }
        
        public KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacionUsuario(int idOrganizacion) {
            return base.Channel.ObtenerOrganizacionUsuario(idOrganizacion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.Organizacion> ObtenerOrganizaciones(System.Nullable<int> idTipoUsuario, System.Nullable<int> idHolding, System.Nullable<int> idCompania, System.Nullable<int> idDireccion, System.Nullable<int> idSubDireccion, System.Nullable<int> idGerencia, System.Nullable<int> idSubGerencia, System.Nullable<int> idJefatura) {
            return base.Channel.ObtenerOrganizaciones(idTipoUsuario, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
        }
        
        public void HabilitarOrganizacion(int idOrganizacion, bool habilitado) {
            base.Channel.HabilitarOrganizacion(idOrganizacion, habilitado);
        }
        
        public KiiniNet.Entities.Cat.Operacion.Organizacion ObtenerOrganizacionById(int idOrganizacion) {
            return base.Channel.ObtenerOrganizacionById(idOrganizacion);
        }
        
        public void ActualizarOrganizacion(KiiniNet.Entities.Cat.Operacion.Organizacion org) {
            base.Channel.ActualizarOrganizacion(org);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.Organizacion> ObtenerOrganizacionesGrupos(System.Collections.Generic.List<int> grupos) {
            return base.Channel.ObtenerOrganizacionesGrupos(grupos);
        }
        
        public System.Collections.Generic.List<int> ObtenerOrganizacionesByIdOrganizacion(int idUbicacion) {
            return base.Channel.ObtenerOrganizacionesByIdOrganizacion(idUbicacion);
        }
    }
}
