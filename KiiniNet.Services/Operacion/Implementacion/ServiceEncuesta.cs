using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceEncuesta : IServiceEncuesta
    {
        public List<Encuesta> ObtenerEncuestas(bool insertarSeleccion)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    return negocio.ObtenerEncuestas(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Encuesta ObtenerEncuestaById(int idEncuesta)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    return negocio.ObtenerEncuestaById(idEncuesta);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Encuesta ObtenerEncuestaByIdTicket(int idTicket)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    return negocio.ObtenerEncuestaByIdTicket(idTicket);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarEncuesta(Encuesta encuesta)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    negocio.GuardarEncuesta(encuesta);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Encuesta> Consulta(string descripcion)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                   return negocio.Consulta(descripcion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarEncuesta(int idencuesta, bool habilitado)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    negocio.HabilitarEncuesta(idencuesta, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperEncuesta> ObtenerEncuestasPendientesUsuario(int idUsuario)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    return negocio.ObtenerEncuestasPendientesUsuario(idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Contestaencuesta(List<RespuestaEncuesta> encuestaRespondida)
        {
            try
            {
                using (BusinessEncuesta negocio = new BusinessEncuesta())
                {
                    negocio.Contestaencuesta(encuestaRespondida);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
