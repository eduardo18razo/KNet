﻿using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Usuarios;

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
        List<GrupoUsuario> ObtenerGruposUsuarioByIdRolTipoUsuario(int idRol, int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, bool insertarSeleccion);

        [OperationContract]
        void GuardarGrupoUsuario(GrupoUsuario grupoUsuario);

        [OperationContract]
        GrupoUsuario ObtenerGrupoUsuarioById(int idGrupoUsuario);
        
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioSistema(int idTipoUsuario);
        
        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioNivel(int idtipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7);

        [OperationContract]
        List<UsuarioGrupo> ObtenerGruposDeUsuario(int idUsuario);
        
        [OperationContract]
        void HabilitarGrupo(int idGrupo, bool habilitado);

        [OperationContract]
        List<GrupoUsuario> ObtenerGruposUsuarioAll(int? idTipoUsuario, int? idTipoGrupo);

        [OperationContract]
        void ActualizarGrupo(GrupoUsuario gpo);

        [OperationContract]
        List<HorarioSubGrupo> ObtenerHorariosByIdSubGrupo(int idSubGrupo);

        [OperationContract]
        List<DiaFestivoSubGrupo> ObtenerDiasByIdSubGrupo(int idSubGrupo);
    }
}
