﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Business.Utils;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceInformacionConsulta : IServiceInformacionConsulta
    {
        public List<InformacionConsulta> ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta tipoinfoConsulta, bool insertarSeleccion)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    return negocio.ObtenerInformacionConsulta(tipoinfoConsulta, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<InformacionConsulta> ObtenerInformacionConsultaArbol(int idArbol)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    return negocio.ObtenerInformacionConsultaArbol(idArbol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public InformacionConsulta ObtenerInformacionConsultaById(int idInformacion)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    return negocio.ObtenerInformacionConsultaById(idInformacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarInformacionConsulta(InformacionConsulta informacion)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    negocio.GuardarInformacionConsulta(informacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarInformacionConsulta(int idInformacionConsulta, InformacionConsulta informacion)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    negocio.ActualizarInformacionConsulta(idInformacionConsulta, informacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarHit(int idArbol, int idUsuario)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    negocio.GuardarHit(idArbol, idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<InformacionConsulta> ObtenerConsulta(int? idTipoInformacionConsulta, int? idTipoDocumento)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    return negocio.ObtenerInformacionConsulta(idTipoInformacionConsulta, idTipoDocumento);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarInformacion(int idInformacion, bool habilitado)
        {
            try
            {
                using (BusinessInformacionConsulta negocio = new BusinessInformacionConsulta())
                {
                    negocio.HabilitarInformacion(idInformacion, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
