using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceArea
    {
        [OperationContract]
        List<Area> ObtenerAreasUsuario(int idUsuario);

        [OperationContract]
        List<Area> ObtenerAreas(bool insertarSeleccion);
    }
}
