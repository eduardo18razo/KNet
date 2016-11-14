using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;
using KiiniNet.Services.Sistema.Interface;
using KinniNet.Core.Sistema;

namespace KiiniNet.Services.Sistema.Implementacion
{
    
    public class ServiceCatalogos : IServiceCatalogos
    {
        public void CrearCatalogo(string nombreCatalogo, bool esMascara)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    negocio.CrearCatalogo(nombreCatalogo, esMascara);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Catalogos> ObtenerCatalogos(bool insertarSeleccion)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    return negocio.ObtenerCatalogos(insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Catalogos> ObtenerCatalogoConsulta(int? idCatalogo)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    return negocio.ObtenerCatalogoConsulta(idCatalogo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public void Habilitar(int idCatalogo, bool habilitado)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    negocio.Habilitar(idCatalogo, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AgregarRegistro(int idCatalogo, string descripcion)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    negocio.AgregarRegistro(idCatalogo, descripcion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CatalogoGenerico> ObtenerRegistrosCatalogo(int idCatalogo)
        {
            try
            {
                using (BusinessCatalogos negocio = new BusinessCatalogos())
                {
                    return negocio.ObtenerRegistrosCatalogo(idCatalogo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
