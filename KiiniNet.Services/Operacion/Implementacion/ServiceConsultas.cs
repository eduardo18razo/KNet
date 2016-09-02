using System;
using System.Collections.Generic;
using KiiniNet.Entities.Helper;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceConsultas : IServiceConsultas
    { 
        public List<HelperTickets> ConsultarTickets(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol,
            List<int> tipificacion, List<int> prioridad, List<int> estatus, bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            try
            {
                using (BusinessConsultas negocio = new BusinessConsultas())
                {
                    return negocio.ConsultarTickets(idUsuario, grupos, organizaciones, ubicaciones, tipoArbol, tipificacion, prioridad, estatus, sla, fechas, pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperHits> ConsultarHits(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipificacion,
            Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            try
            {
                using (BusinessConsultas negocio = new BusinessConsultas())
                {
                    return negocio.ConsultarHits(idUsuario, grupos, organizaciones, ubicaciones, tipificacion, fechas, pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperTickets> ConsultarEncuestas(int idUsuario, List<int> grupos, List<int> organizaciones, List<int> ubicaciones, List<int> tipoArbol,
            List<int> tipificacion, List<int> prioridad, List<int> estatus, bool sla, Dictionary<string, DateTime> fechas, int pageIndex, int pageSize)
        {
            try
            {
                using (BusinessConsultas negocio = new BusinessConsultas())
                {
                    return negocio.ConsultarEncuestas(idUsuario, grupos, organizaciones, ubicaciones, tipoArbol, tipificacion, prioridad, estatus, sla, fechas, pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
