using KiiniNet.Services.Sistema.Interface;
using System;
using System.Collections.Generic;
using KinniNet.Core.Sistema;
using KiiniNet.Entities.Parametros;

namespace KiiniNet.Services.Sistema.Implementacion
{
    public class ServicePoliticas : IServicePoliticas
    {
        public List<EstatusAsignacionSubRolGeneralDefault> GeneraEstatusAsignacionGrupoDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.GeneraEstatusAsignacionGrupoDefault();
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

        public List<EstatusTicketSubRolGeneralDefault> GeneraEstatusTicketSubRolGeneralDefault()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.GeneraEstatusTicketSubRolGeneralDefault();
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

        public List<SubRolEscalacionPermitida> GeneraSubRolEscalacionPermitida()
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    return negocio.GeneraSubRolEscalacionPermitida();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarPoliticaEscalacion(int idEscalacion, bool habilitado)
        {
            try
            {
                using (BusinessPoliticas negocio = new BusinessPoliticas())
                {
                    negocio.HabilitarPoliticaEscalacion(idEscalacion, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
