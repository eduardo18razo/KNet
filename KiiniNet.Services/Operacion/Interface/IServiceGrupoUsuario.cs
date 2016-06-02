using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceGrupoUsuario
    {
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuario(int idTipoGrupo, bool insertarSeleccion);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioByIdTipoSubGrupo(int idTipoSubgrupo, bool insertarSeleccion);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, bool insertarSeleccion);

        [OperationContract]
        void GuardarGrupoUsuario(GrupoUsuario grupoUsuario);

        [OperationContract]
        GrupoUsuario ObtenerGrupoUsuario(int idGrupoUsuario);
        [OperationContract]
        GrupoUsuario ObtenerGrupoUsuarioById(int idGrupoUsuario);
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioSistema();
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioNivel(int idtipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7);
    }
}
