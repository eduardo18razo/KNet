using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServiceNotificacion
    {
        [OperationContract]
        List<TipoNotificacion> ObtenerTipos(bool insertarSeleccion);
    }
}
