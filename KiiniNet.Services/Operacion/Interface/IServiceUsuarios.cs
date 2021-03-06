﻿using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceUsuarios
    {
        [OperationContract]
        void GuardarUsuario(Usuario usuario);

        [OperationContract]
        List<Usuario> ObtenerUsuarios(int? idTipoUsuario);

        [OperationContract]
        Usuario ObtenerDetalleUsuario(int idUsuario);

        [OperationContract]
        List<Usuario> ObtenerUsuariosByGrupo(int idGrupo, int idNivel);

        [OperationContract]
        void ActualizarUsuario(int idUsuario, Usuario usuario);

        [OperationContract]
        List<Usuario> ObtenerAtendedoresEncuesta(int idUsuario, List<int?> encuestas);

        [OperationContract]
        bool ValidaUserName(string nombreUsuario);

        [OperationContract]
        bool ValidaConfirmacion(int idUsuario, string guid);

        [OperationContract]
        string ValidaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo);

        [OperationContract]
        void EnviaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono);

        [OperationContract]
        void ActualizarTelefono(int idUsuario, int idTelefono, string numero);

        [OperationContract]
        void ConfirmaCuenta(int idUsuario, string password, Dictionary<int, string> confirmaciones, List<PreguntaReto> pregunta, string link);

        [OperationContract]
        Usuario BuscarUsuario(string usuario);

        [OperationContract]
        string EnviaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, int idCorreo);

        [OperationContract]
        void ValidaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, int idCorreo, string codigo);

        [OperationContract]
        void ValidaRespuestasReto(int idUsuario, Dictionary<int, string> preguntasReto);
    }
}
