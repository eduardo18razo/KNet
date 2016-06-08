using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceGrupoUsuario
    {
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioTipoUsuario(int idTipoGrupo, int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioByIdTipoSubGrupo(int idTipoSubgrupo, bool insertarSeleccion);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        void GuardarGrupoUsuario(GrupoUsuario grupoUsuario);

        [OperationContract]
        GrupoUsuario ObtenerGrupoUsuarioById(int idGrupoUsuario);
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioSistema(int idTipoUsuario);
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioNivel(int idtipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7);
    }
}
