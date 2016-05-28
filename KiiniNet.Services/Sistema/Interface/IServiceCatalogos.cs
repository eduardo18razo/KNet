using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Services.Sistema.Interface
{
    [ServiceContract]
    public interface IServiceCatalogos
    {
        [OperationContract]
        List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion);
    }
}
