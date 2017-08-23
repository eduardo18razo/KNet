using KiiniNet.Entities.Parametros;
using System.Collections.Generic;
using System.ServiceModel;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServicePoliticas
    {
        [OperationContract]
        List<EstatusAsignacionSubRolGeneralDefault> EstatusAsignacionSubRolGeneralDefault();
        [OperationContract]
        List<EstatusTicketSubRolGeneralDefault> EstatusTicketSubRolGeneralDefault();

        [OperationContract]
        List<EstatusAsignacionSubRolGeneral> EstatusAsignacionSubRolGeneral();
        [OperationContract]
        List<EstatusTicketSubRolGeneral> EstatusTicketSubRolGeneral();

        [OperationContract]
        void HabilitarPoliticaAsignacion(int idArea, bool habilitado);
        [OperationContract]
        void HabilitarPoliticaEstatus(int idArea, bool habilitado);
    }


}
