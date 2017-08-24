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
        List<EstatusAsignacionSubRolGeneralDefault> GeneraEstatusAsignacionGrupoDefault();

        [OperationContract]
        void HabilitarPoliticaAsignacion(int idArea, bool habilitado);
        [OperationContract]
        List<EstatusTicketSubRolGeneralDefault> GeneraEstatusTicketSubRolGeneralDefault();

        [OperationContract]
        void HabilitarPoliticaEstatus(int idArea, bool habilitado);


        [OperationContract]
        List<SubRolEscalacionPermitida> GeneraSubRolEscalacionPermitida();

        [OperationContract]
        void HabilitarPoliticaEscalacion(int idEscalacion, bool habilitado);
    }

     
}
