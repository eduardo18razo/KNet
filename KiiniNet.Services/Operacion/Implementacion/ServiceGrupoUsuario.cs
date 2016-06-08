﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceGrupoUsuario : IServiceGrupoUsuario 
    {
        public List<GrupoUsuario> ObtenerGruposUsuarioByIdTipoSubGrupo(int idTipoSubgrupo, bool insertarSeleccion)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioByIdTipoSubGrupo(idTipoSubgrupo, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, int idTipoUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioByIdRol(idRol, idTipoUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoUsuario> ObtenerGruposUsuarioTipoUsuario(int idTipoGrupo, int idTipoUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioTipoUsuario(idTipoGrupo, idTipoUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GuardarGrupoUsuario(GrupoUsuario grupoUsuario)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    negocio.GuardarGrupoUsuario(grupoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public GrupoUsuario ObtenerGrupoUsuarioById(int idGrupoUsuario)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGrupoUsuario(idGrupoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoUsuario> ObtenerGruposUsuarioSistema(int idTipoUsuario)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioSistema(idTipoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoUsuario> ObtenerGruposUsuarioNivel(int idtipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioNivel(idtipoArbol,  nivel1,  nivel2,  nivel3,  nivel4,  nivel5,  nivel6,  nivel7);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
