using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceEncuesta
    {
        [OperationContract]
        List<Encuesta> ObtenerEncuestas(bool insertarSeleccion);

        [OperationContract]
        void GuardarEncuesta(Encuesta encuesta);

        [OperationContract]
        List<Encuesta> Consulta(string descripcion);

        [OperationContract]
        void HabilitarEncuesta(int idencuesta, bool habilitado);
    }
}
