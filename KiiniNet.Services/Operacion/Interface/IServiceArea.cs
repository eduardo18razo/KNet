using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceArea
    {
        [OperationContract]
        List<Area> ObtenerAreasUsuario(int idUsuario, bool insertarSeleccion);

        [OperationContract]
        List<Area> ObtenerAreasUsuarioByRol(int idUsuario, bool insertarSeleccion);

        [OperationContract]
        List<Area> ObtenerAreasUsuarioTercero(int idUsuario, int idUsuarioTercero, bool insertarSeleccion);

        [OperationContract]
        List<Area> ObtenerAreasUsuarioPublico(bool insertarSeleccion);
        
        [OperationContract]
        List<Area> ObtenerAreasTipoUsuario(int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        List<Area> ObtenerAreas(bool insertarSeleccion);

        [OperationContract]
        void Guardar(Area area);
    }
}
