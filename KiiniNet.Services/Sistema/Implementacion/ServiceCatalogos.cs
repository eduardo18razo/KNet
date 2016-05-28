using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    
    public class ServiceCatalogos : IServiceCatalogos
    {
        public List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    return negocio.ObtenerCatalogosMascaraCaptura(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
