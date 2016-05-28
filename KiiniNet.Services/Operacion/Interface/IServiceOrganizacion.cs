﻿using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceOrganizacion
    {
        [OperationContract]
        List<Holding> ObtenerHoldings(int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        List<Compania> ObtenerCompañias(int idTipoUsuario, int idHolding, bool insertarSeleccion);

        [OperationContract]
        List<Direccion> ObtenerDirecciones(int idTipoUsuario, int idCompañia, bool insertarSeleccion);

        [OperationContract]
        List<SubDireccion> ObtenerSubDirecciones(int idTipoUsuario, int idDireccoin, bool insertarSeleccion);

        [OperationContract]
        List<Gerencia> ObtenerGerencias(int idTipoUsuario, int idSubdireccion, bool insertarSeleccion);

        [OperationContract]
        List<SubGerencia> ObtenerSubGerencias(int idTipoUsuario, int idGerencia, bool insertarSeleccion);

        [OperationContract]
        List<Jefatura> ObtenerJefaturas(int idTipoUsuario, int idSubGerencia, bool insertarSeleccion);

        [OperationContract]
        Organizacion ObtenerOrganizacion(int idHolding, int? idCompania, int? idDireccion, int? idSubDireccion,int? idGerencia, int? idSubGerencia, int? idJefatura);

        [OperationContract]
        void GuardarOrganizacion(Organizacion organizacion);

        [OperationContract]
        void GuardarHolding(Holding entidad);

        [OperationContract]
        void GuardarCompania(Compania entidad);

        [OperationContract]
        void GuardarDireccion(Direccion entidad);

        [OperationContract]
        void GuardarSubDireccion(SubDireccion entidad);

        [OperationContract]
        void GuardarGerencia(Gerencia entidad);

        [OperationContract]
        void GuardarSubGerencia(SubGerencia entidad);

        [OperationContract]
        void GuardarJefatura(Jefatura entidad);
    }
}
