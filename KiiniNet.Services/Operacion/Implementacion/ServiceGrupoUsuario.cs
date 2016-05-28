using System;
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

        public List<GrupoUsuario> ObtenerGruposUsuarioByIdRol(int idRol, bool insertarSeleccion)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuarioByIdRol(idRol, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoUsuario> ObtenerGruposUsuario(int idTipoGrupo, bool insertarSeleccion)
        {
            try
            {
                using (BusinessGrupoUsuario negocio = new BusinessGrupoUsuario())
                {
                    return negocio.ObtenerGruposUsuario(idTipoGrupo, insertarSeleccion);
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

        public GrupoUsuario ObtenerGrupoUsuario(int idGrupoUsuario)
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
    }
}
