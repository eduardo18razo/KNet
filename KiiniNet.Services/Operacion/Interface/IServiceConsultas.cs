using System;
using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceConsultas
    {
        [OperationContract]
        List<HelperTickets> ConsultarTickets(int idUsuario, List<int> grupos, List<int> organizaciones,
            List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus,
            bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize);

        [OperationContract]
        List<HelperHits> ConsultarHits(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones,
            List<int> tipificacion, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize);

        [OperationContract]
        List<HelperTickets> ConsultarEncuestas(int idUsuario, List<int> grupos, List<int> organizaciones,
            List<int> ubicaciones, List<int> tipoArbol, List<int> tipificacion, List<int> prioridad, List<int> estatus,
            bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize);
    }
}
