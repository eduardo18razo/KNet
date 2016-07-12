﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiiniHelp.ServiceArbolAcceso {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceArbolAcceso.IServiceArbolAcceso")]
    public interface IServiceArbolAcceso {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel1", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel1Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel1> ObtenerNivel1(int idTipoArbol, int idTipoUsuario, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel2", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel2Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel2> ObtenerNivel2(int idTipoArbol, int idTipoUsuario, int idNivel1, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel3", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel3Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel3> ObtenerNivel3(int idTipoArbol, int idTipoUsuario, int idNivel2, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel4", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel4Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel4> ObtenerNivel4(int idTipoArbol, int idTipoUsuario, int idNivel3, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel5", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel5Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel5> ObtenerNivel5(int idTipoArbol, int idTipoUsuario, int idNivel4, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel6", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel6Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel6> ObtenerNivel6(int idTipoArbol, int idTipoUsuario, int idNivel5, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel7", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerNivel7Response")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel7> ObtenerNivel7(int idTipoArbol, int idTipoUsuario, int idNivel6, bool insertarSeleccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/EsNodoTerminal", ReplyAction="http://tempuri.org/IServiceArbolAcceso/EsNodoTerminalResponse")]
        bool EsNodoTerminal(int idTipoUsuario, int idTipoArbol, int nivel1, System.Nullable<int> nivel2, System.Nullable<int> nivel3, System.Nullable<int> nivel4, System.Nullable<int> nivel5, System.Nullable<int> nivel6, System.Nullable<int> nivel7);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/GuardarArbol", ReplyAction="http://tempuri.org/IServiceArbolAcceso/GuardarArbolResponse")]
        void GuardarArbol(KiiniNet.Entities.Cat.Operacion.ArbolAcceso arbol);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerArblodesAccesoByGruposUsuario", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerArblodesAccesoByGruposUsuarioRespon" +
            "se")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.ArbolAcceso> ObtenerArblodesAccesoByGruposUsuario(int idUsuario, int idTipoArbol, int idArea);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerArbolAcceso", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerArbolAccesoResponse")]
        KiiniNet.Entities.Cat.Operacion.ArbolAcceso ObtenerArbolAcceso(int idArbol);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ObtenerArbolesAccesoAll", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ObtenerArbolesAccesoAllResponse")]
        System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.ArbolAcceso> ObtenerArbolesAccesoAll(System.Nullable<int> idArea, System.Nullable<int> idTipoUsuario, System.Nullable<int> idTipoArbol, System.Nullable<int> nivel1, System.Nullable<int> nivel2, System.Nullable<int> nivel3, System.Nullable<int> nivel4, System.Nullable<int> nivel5, System.Nullable<int> nivel6, System.Nullable<int> nivel7);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/HabilitarArbol", ReplyAction="http://tempuri.org/IServiceArbolAcceso/HabilitarArbolResponse")]
        void HabilitarArbol(int idArbol, bool habilitado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceArbolAcceso/ActualizardArbol", ReplyAction="http://tempuri.org/IServiceArbolAcceso/ActualizardArbolResponse")]
        void ActualizardArbol(KiiniNet.Entities.Cat.Operacion.ArbolAcceso arbolAcceso);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceArbolAccesoChannel : KiiniHelp.ServiceArbolAcceso.IServiceArbolAcceso, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceArbolAccesoClient : System.ServiceModel.ClientBase<KiiniHelp.ServiceArbolAcceso.IServiceArbolAcceso>, KiiniHelp.ServiceArbolAcceso.IServiceArbolAcceso {
        
        public ServiceArbolAccesoClient() {
        }
        
        public ServiceArbolAccesoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceArbolAccesoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceArbolAccesoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceArbolAccesoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel1> ObtenerNivel1(int idTipoArbol, int idTipoUsuario, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel1(idTipoArbol, idTipoUsuario, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel2> ObtenerNivel2(int idTipoArbol, int idTipoUsuario, int idNivel1, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel2(idTipoArbol, idTipoUsuario, idNivel1, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel3> ObtenerNivel3(int idTipoArbol, int idTipoUsuario, int idNivel2, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel3(idTipoArbol, idTipoUsuario, idNivel2, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel4> ObtenerNivel4(int idTipoArbol, int idTipoUsuario, int idNivel3, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel4(idTipoArbol, idTipoUsuario, idNivel3, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel5> ObtenerNivel5(int idTipoArbol, int idTipoUsuario, int idNivel4, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel5(idTipoArbol, idTipoUsuario, idNivel4, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel6> ObtenerNivel6(int idTipoArbol, int idTipoUsuario, int idNivel5, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel6(idTipoArbol, idTipoUsuario, idNivel5, insertarSeleccion);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Arbol.Nodos.Nivel7> ObtenerNivel7(int idTipoArbol, int idTipoUsuario, int idNivel6, bool insertarSeleccion) {
            return base.Channel.ObtenerNivel7(idTipoArbol, idTipoUsuario, idNivel6, insertarSeleccion);
        }
        
        public bool EsNodoTerminal(int idTipoUsuario, int idTipoArbol, int nivel1, System.Nullable<int> nivel2, System.Nullable<int> nivel3, System.Nullable<int> nivel4, System.Nullable<int> nivel5, System.Nullable<int> nivel6, System.Nullable<int> nivel7) {
            return base.Channel.EsNodoTerminal(idTipoUsuario, idTipoArbol, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7);
        }
        
        public void GuardarArbol(KiiniNet.Entities.Cat.Operacion.ArbolAcceso arbol) {
            base.Channel.GuardarArbol(arbol);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.ArbolAcceso> ObtenerArblodesAccesoByGruposUsuario(int idUsuario, int idTipoArbol, int idArea) {
            return base.Channel.ObtenerArblodesAccesoByGruposUsuario(idUsuario, idTipoArbol, idArea);
        }
        
        public KiiniNet.Entities.Cat.Operacion.ArbolAcceso ObtenerArbolAcceso(int idArbol) {
            return base.Channel.ObtenerArbolAcceso(idArbol);
        }
        
        public System.Collections.Generic.List<KiiniNet.Entities.Cat.Operacion.ArbolAcceso> ObtenerArbolesAccesoAll(System.Nullable<int> idArea, System.Nullable<int> idTipoUsuario, System.Nullable<int> idTipoArbol, System.Nullable<int> nivel1, System.Nullable<int> nivel2, System.Nullable<int> nivel3, System.Nullable<int> nivel4, System.Nullable<int> nivel5, System.Nullable<int> nivel6, System.Nullable<int> nivel7) {
            return base.Channel.ObtenerArbolesAccesoAll(idArea, idTipoUsuario, idTipoArbol, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7);
        }
        
        public void HabilitarArbol(int idArbol, bool habilitado) {
            base.Channel.HabilitarArbol(idArbol, habilitado);
        }
        
        public void ActualizardArbol(KiiniNet.Entities.Cat.Operacion.ArbolAcceso arbolAcceso) {
            base.Channel.ActualizardArbol(arbolAcceso);
        }
    }
}
