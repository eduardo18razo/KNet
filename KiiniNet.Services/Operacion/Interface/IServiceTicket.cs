using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceTicket
    {
        [OperationContract]
        void Guardar(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura);
    }

}
