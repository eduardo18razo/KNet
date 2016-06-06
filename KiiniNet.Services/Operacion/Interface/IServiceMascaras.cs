using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Mascaras;
using KinniNet.Core.Operacion;

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
        List<Mascara> ObtenerMascarasAcceso(bool insertarSeleccion);
        [OperationContract]
        List<BusinessMascaras.CatalogoGenerico> ObtenerCatalogoCampoMascara(string tabla);
    }
}
