using KiiniNet.Services.Sistema.Interface;
using System;
using System.Collections.Generic;
using KinniNet.Core.Sistema;
using KiiniNet.Entities.Parametros;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServicePoliticas : IServicePoliticas
    {
        public List<EstatusAsignacionSubRolGeneralDefault> EstatusAsignacionSubRolGeneralDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.EstatusAsignacionSubRolGeneralDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusTicketSubRolGeneralDefault> EstatusTicketSubRolGeneralDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.EstatusTicketSubRolGeneralDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusAsignacionSubRolGeneral> EstatusAsignacionSubRolGeneral()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.EstatusAsignacionSubRolGeneral();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstatusTicketSubRolGeneral> EstatusTicketSubRolGeneral()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.EstatusTicketSubRolGeneral();
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
