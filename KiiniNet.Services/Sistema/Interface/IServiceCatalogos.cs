using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServiceCatalogos
    {
        [OperationContract]
        void CrearCatalogo(string nombreCatalogo, bool esMascara, List<string> registros);

        [OperationContract]
        Catalogos ObtenerCatalogo(int idCatalogo);
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
        List<CatalogoGenerico> ObtenerRegistrosSistemaCatalogo(int idCatalogo, bool insertarSeleccion);
        [OperationContract]
        DataTable ObtenerRegistrosArchivosCatalogo(int idCatalogo);

        [OperationContract]
        void CrearCatalogoExcel(string nombreCatalogo, bool esMascara, string archivo, string hoja);

        [OperationContract]
        void ActualizarCatalogoExcel(int idCatalogo, bool esMascara, string archivo, string hoja);
    }
}
