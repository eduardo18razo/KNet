using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceTicket
    {
        [OperationContract]
        void CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura);

        [OperationContract]
        List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize);
    }

}
