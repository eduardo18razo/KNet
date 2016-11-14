using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Helper;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceUsuarios" en el código y en el archivo de configuración a la vez.
    public class ServiceMascaras : IServiceMascaras
    {
        public void CrearMascara(Mascara mascara)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    negocio.CrearMascara(mascara);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Mascara ObtenerMascaraCaptura(int idMascara)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.ObtenerMascaraCaptura(idMascara);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Mascara ObtenerMascaraCapturaByIdTicket(int idTicket)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.ObtenerMascaraCapturaByIdTicket(idTicket);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Mascara> ObtenerMascarasAcceso(bool insertarSeleccion)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.ObtenerMascarasAcceso(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CatalogoGenerico> ObtenerCatalogoCampoMascara(string tabla)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.ObtenerCatalogoCampoMascara(tabla);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Mascara> Consulta(string descripcion)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.Consulta(descripcion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarMascara(int idMascara, bool habilitado)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    negocio.HabilitarMascara(idMascara, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HelperMascaraData> ObtenerDatosMascara(int idMascara, int idTicket)
        {
            try
            {
                using (BusinessMascaras negocio = new BusinessMascaras())
                {
                    return negocio.ObtenerDatosMascara(idMascara, idTicket);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
