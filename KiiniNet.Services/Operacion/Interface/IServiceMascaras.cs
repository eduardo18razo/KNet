using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Operacion.Interface
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceUsuarios" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceMascaras
    {
        [OperationContract]
        void CrearMascara(Mascara mascara);

        [OperationContract]
        Mascara ObtenerMascaraCaptura(int idMascara);
        [OperationContract]
        Mascara ObtenerMascaraCapturaByIdTicket(int idTicket);

        [OperationContract]
        List<Mascara> ObtenerMascarasAcceso(bool insertarSeleccion);
        [OperationContract]
        List<CatalogoGenerico> ObtenerCatalogoCampoMascara(string tabla);

        [OperationContract]
        List<Mascara> Consulta(string descripcion);

        [OperationContract]
        void HabilitarMascara(int idMascara, bool habilitado);

        [OperationContract]
        List<HelperMascaraData> ObtenerDatosMascara(int idMascara, int idTicket);
    }
}
