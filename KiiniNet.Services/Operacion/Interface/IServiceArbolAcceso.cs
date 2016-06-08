using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceArbolAcceso
    {
        [OperationContract]
        List<Nivel1> ObtenerNivel1(int idTipoArbol, int idTipoUsuario, bool insertarSeleccion);
        [OperationContract]
        List<Nivel2> ObtenerNivel2(int idTipoArbol, int idTipoUsuario, int idNivel1, bool insertarSeleccion);
        [OperationContract]
        List<Nivel3> ObtenerNivel3(int idTipoArbol, int idTipoUsuario, int idNivel2, bool insertarSeleccion);
        [OperationContract]
        List<Nivel4> ObtenerNivel4(int idTipoArbol, int idTipoUsuario, int idNivel3, bool insertarSeleccion);
        [OperationContract]
        List<Nivel5> ObtenerNivel5(int idTipoArbol, int idTipoUsuario, int idNivel4, bool insertarSeleccion);
        [OperationContract]
        List<Nivel6> ObtenerNivel6(int idTipoArbol, int idTipoUsuario, int idNivel5, bool insertarSeleccion);
        [OperationContract]
        List<Nivel7> ObtenerNivel7(int idTipoArbol, int idTipoUsuario, int idNivel6, bool insertarSeleccion);
        [OperationContract]
        bool EsNodoTerminal(int idTipoUsuario, int idTipoArbol, int nivel1, int? nivel2, int? nivel3, int? nivel4,int? nivel5, int? nivel6, int? nivel7);
        [OperationContract]
        void GuardarArbol(ArbolAcceso arbol);
        [OperationContract]
        List<ArbolAcceso> ObtenerArblodesAccesoByGruposUsuario(int idUsuario, int idTipoArbol, int idArea);
        [OperationContract]
        ArbolAcceso ObtenerArbolAcceso(int idArbol);
    }
}
