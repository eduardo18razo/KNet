using KiiniNet.Services.Sistema.Interface;
using System;
using System.Collections.Generic;
using KinniNet.Core.Sistema;
using KiiniNet.Entities.Parametros;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServicePoliticas : IServicePoliticas
    {
        public List<EstatusAsignacionSubRolGeneralDefault> GeneraEstatusAsignacionSubRolGeneralDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.GeneraEstatusAsignacionSubRolGeneralDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusTicketSubRolGeneralDefault> ObtenerEstatusTicketSubRolGeneralDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.ObtenerEstatusTicketSubRolGeneralDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusAsignacionSubRolGeneral> ObtenerEstatusAsignacionSubRolGeneral()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.ObtenerEstatusAsignacionSubRolGeneral();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusTicketSubRolGeneral> ObtenerEstatusTicketSubRolGeneral()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.ObtenerEstatusTicketSubRolGeneral();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarPoliticaAsignacion(int idAsignacion, bool habilitado)
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    negocio.HabilitarPoliticaAsignacion(idAsignacion, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarPoliticaEstatus(int idAsignacion, bool habilitado)
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    negocio.HabilitarPoliticaEstatus(idAsignacion, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
