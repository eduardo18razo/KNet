using KiiniNet.Entities.Parametros;
using System.Collections.Generic;
using System.ServiceModel;

namespace KiiniNet.Services.Sistema.Interface
{
    //
    [ServiceContract]
    public interface IServicePoliticas
    {
        [OperationContract]
        List<EstatusAsignacionSubRolGeneralDefault> ObtenerEstatusAsignacionSubRolGeneralDefault();
        [OperationContract]
        List<EstatusTicketSubRolGeneralDefault> ObtenerEstatusTicketSubRolGeneralDefault();
        [OperationContract]
        List<EstatusAsignacionSubRolGeneral> ObtenerEstatusAsignacionSubRolGeneral();
        [OperationContract]
        List<EstatusTicketSubRolGeneral> ObtenerEstatusTicketSubRolGeneral();

        [OperationContract]
        void HabilitarPoliticaAsignacion(int idAsignacion, bool habilitado);
        

        [OperationContract]
        void HabilitarPoliticaEstatus(int idAsignacion, bool habilitado);


        

        
    }

     
}
