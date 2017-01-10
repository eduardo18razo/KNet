﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public  class ServiceArea : IServiceArea
    {
        public List<Area> ObtenerAreasUsuario(int idUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuario(idUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasUsuarioByRol(int idUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuarioByRol(idUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasUsuarioTercero(int idUsuario, int idUsuarioTercero, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuarioTercero(idUsuario, idUsuarioTercero, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasUsuarioPublico(bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasUsuarioPublico(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreasTipoUsuario(int idTipoUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreasTipoUsuario(idTipoUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Area> ObtenerAreas(bool insertarSeleccion)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    return negocio.ObtenerAreas(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Guardar(Area area)
        {
            try
            {
                using (BusinessArea negocio = new BusinessArea())
                {
                    negocio.Guardar(area);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}