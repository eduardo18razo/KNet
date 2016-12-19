using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceDiasHorario
    {
        [OperationContract]
        List<Horario> ObtenerHorarioDefault(bool insertarSeleccion);
        [OperationContract]
        List<Horario> ObtenerHorarioConsulta(int? idGrupoSolicito);

        [OperationContract]
        void CrearHorario(Horario horario);

        [OperationContract]
        void Habilitar(int idHorario, bool habilitado);
    }
}
