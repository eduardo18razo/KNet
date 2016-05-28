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
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;

namespace KiiniHelp.ServiceInformacionConsulta {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceInformacionConsulta.IServiceInformacionConsulta")]
    public interface IServiceInformacionConsulta {
        
        [OperationContract(Action="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsulta", ReplyAction="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsultaResponse" +
            "")]
        List<InformacionConsulta> ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta tipoinfoConsulta, bool insertarSeleccion);
        
        [OperationContract(Action="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsultaArbol", ReplyAction="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsultaArbolRes" +
            "ponse")]
        List<InformacionConsulta> ObtenerInformacionConsultaArbol(int idArbol);
        
        [OperationContract(Action="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsultaById", ReplyAction="http://tempuri.org/IServiceInformacionConsulta/ObtenerInformacionConsultaByIdResp" +
            "onse")]
        InformacionConsulta ObtenerInformacionConsultaById(int idInformacion);
        
        [OperationContract(Action="http://tempuri.org/IServiceInformacionConsulta/GuardarInformacionConsulta", ReplyAction="http://tempuri.org/IServiceInformacionConsulta/GuardarInformacionConsultaResponse" +
            "")]
        void GuardarInformacionConsulta(InformacionConsulta informacion);
        
        [OperationContract(Action="http://tempuri.org/IServiceInformacionConsulta/GuardarHit", ReplyAction="http://tempuri.org/IServiceInformacionConsulta/GuardarHitResponse")]
        void GuardarHit(int idArbol, int idUsuario);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IServiceInformacionConsultaChannel : IServiceInformacionConsulta, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceInformacionConsultaClient : ClientBase<IServiceInformacionConsulta>, IServiceInformacionConsulta {
        
        public ServiceInformacionConsultaClient() {
        }
        
        public ServiceInformacionConsultaClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceInformacionConsultaClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInformacionConsultaClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInformacionConsultaClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<InformacionConsulta> ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta tipoinfoConsulta, bool insertarSeleccion) {
            return base.Channel.ObtenerInformacionConsulta(tipoinfoConsulta, insertarSeleccion);
        }
        
        public List<InformacionConsulta> ObtenerInformacionConsultaArbol(int idArbol) {
            return base.Channel.ObtenerInformacionConsultaArbol(idArbol);
        }
        
        public InformacionConsulta ObtenerInformacionConsultaById(int idInformacion) {
            return base.Channel.ObtenerInformacionConsultaById(idInformacion);
        }
        
        public void GuardarInformacionConsulta(InformacionConsulta informacion) {
            base.Channel.GuardarInformacionConsulta(informacion);
        }
        
        public void GuardarHit(int idArbol, int idUsuario) {
            base.Channel.GuardarHit(idArbol, idUsuario);
        }
    }
}
