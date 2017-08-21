using KiiniNet.Entities.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace KiiniNet.Services.Sistema.Interface
{
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

    }

     
}
