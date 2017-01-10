﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Parametros;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServiceSubRol : IServiceSubRol
    {
        public List<SubRol> ObtenerSubRolesByTipoGrupo(int idTipoGrupo, bool insertarSeleccion)
        {
            try
            {
                using (BusinessSubRol negocio = new BusinessSubRol())
                {
                    return negocio.ObtenerSubRolesByTipoGrupo(idTipoGrupo, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SubRol ObtenerSubRolById(int idSubRol)
        {
            try
            {
                using (BusinessSubRol negocio = new BusinessSubRol())
                {
                    return negocio.ObtenerSubRolById(idSubRol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SubRol> ObtenerSubRolesByGrupoUsuarioRol(int idGrupoUsuario, int idRol, bool insertarSeleccion)
        {

            try
            {
                using (BusinessSubRol negocio = new BusinessSubRol())
                {
                    return negocio.ObtenerSubRolesByGrupoUsuarioRol(idGrupoUsuario, idRol, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SubRol> ObtenerTipoSubRol(int idTipoGrupo, bool insertarSeleccion)
        {
            return null;
        }

        public List<SubRolEscalacionPermitida> ObtenerEscalacion(int idSubRol, int idEstatusAsignacion)
        {
            try
            {
                using (BusinessSubRol negocio = new BusinessSubRol())
                {
                    return negocio.ObtenerEscalacion(idSubRol, idEstatusAsignacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
