using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServiceEstatus
    {
        [OperationContract]
        List<EstatusTicket> ObtenerEstatusTicket(bool insertarSeleccion);
        [OperationContract]
        List<EstatusAsignacion> ObtenerEstatusAsignacion(bool insertarSeleccion);
    }
}
