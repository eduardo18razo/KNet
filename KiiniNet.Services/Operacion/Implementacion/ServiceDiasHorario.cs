using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Parametros;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceDiasHorario : IServiceDiasHorario
    {
        public List<Horario> ObtenerHorarioDefault(bool insertarSeleccion)
        {
            try
            {
                using (BusinessDiasHorario negocio = new BusinessDiasHorario())
                {
                    return negocio.ObtenerHorarioSistema(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Horario> ObtenerHorarioConsulta(int? idGrupoSolicito)
        {
            try
            {
                using (BusinessDiasHorario negocio = new BusinessDiasHorario())
                {
                    return negocio.ObtenerHorarioConsulta(idGrupoSolicito);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CrearHorario(Horario horario)
        {
            try
            {
                using (BusinessDiasHorario negocio = new BusinessDiasHorario())
                {
                    negocio.CrearHorario(horario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Habilitar(int idHorario, bool habilitado)
        {
            try
            {
                using (BusinessDiasHorario negocio = new BusinessDiasHorario())
                {
                    negocio.Habilitar(idHorario, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
