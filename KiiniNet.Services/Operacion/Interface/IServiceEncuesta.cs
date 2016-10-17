using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceEncuesta
    {
        [OperationContract]
        List<Encuesta> ObtenerEncuestas(bool insertarSeleccion);
        [OperationContract]
        Encuesta ObtenerEncuestaById(int idEncuesta);
        [OperationContract]
        Encuesta ObtenerEncuestaByIdTicket(int idTicket);

        [OperationContract]
        void GuardarEncuesta(Encuesta encuesta);

        [OperationContract]
        List<Encuesta> Consulta(string descripcion);

        [OperationContract]
        void HabilitarEncuesta(int idencuesta, bool habilitado);

        [OperationContract]
        List<HelperEncuesta> ObtenerEncuestasPendientesUsuario(int idUsuario);

        [OperationContract]

        void ContestaEncuesta(List<RespuestaEncuesta> encuestaRespondida);

        [OperationContract]
        List<Encuesta> ObtenerEncuestasContestadas(bool insertarSeleccion);

        [OperationContract]
        List<Encuesta> ObtenerEncuestaByGrupos(List<int> grupos, bool insertarSeleccion);

    }
}
