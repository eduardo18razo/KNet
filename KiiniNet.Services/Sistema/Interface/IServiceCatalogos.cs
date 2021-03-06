﻿using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServiceCatalogos
    {
        [OperationContract]
        void CrearCatalogo(string nombreCatalogo, bool esMascara);
        [OperationContract]
        List<Catalogos> ObtenerCatalogos(bool insertarSeleccion);
        [OperationContract]
        List<Catalogos> ObtenerCatalogoConsulta(int? idCatalogo);
        [OperationContract]
        List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion);

        [OperationContract]
        void Habilitar(int idCatalogo, bool habilitado);

        [OperationContract]
        void AgregarRegistro(int idCatalogo, string descripcion);

        [OperationContract]
        List<CatalogoGenerico> ObtenerRegistrosCatalogo(int idCatalogo);
    }
}
