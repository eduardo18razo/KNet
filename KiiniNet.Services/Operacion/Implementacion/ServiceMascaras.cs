using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceUsuarios" en el código y en el archivo de configuración a la vez.
    public class ServiceMascaras : IServiceMascaras
    {
        public void CrearMascara(Mascara mascara)
        {
            using (BusinessMascaras negocio = new BusinessMascaras())
            {
                negocio.CrearMascara(mascara);
            }
        }

        public Mascara ObtenerMascaraCaptura(int idMascara)
        {
            using (BusinessMascaras negocio = new BusinessMascaras())
            {
                return negocio.ObtenerMascaraCaptura(idMascara);
            }
        }

        public List<Mascara> ObtenerMascarasAcceso(bool insertarSeleccion)
        {
            using (BusinessMascaras negocio = new BusinessMascaras())
            {
                return negocio.ObtenerMascarasAcceso(insertarSeleccion);
            }
        }

        public List<BusinessMascaras.CatalogoGenerico> ObtenerCatalogoCampoMascara(string tabla)
        {
            using (BusinessMascaras negocio = new BusinessMascaras())
            {
                return negocio.ObtenerCatalogoCampoMascara(tabla);
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
    }
}
