using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceUbicacion
    {
        [OperationContract]
        List<Pais> ObtenerPais(int idTipoUsuario, bool insertarSeleccion);

        [OperationContract]
        List<Campus> ObtenerCampus(int idTipoUsuario, int idPais, bool insertarSeleccion);

        [OperationContract]
        List<Torre> ObtenerTorres(int idTipoUsuario, int idCampus, bool insertarSeleccion);

        [OperationContract]
        List<Piso> ObtenerPisos(int idTipoUsuario, int idTorre, bool insertarSeleccion);

        [OperationContract]
        List<Zona> ObtenerZonas(int idTipoUsuario, int idPiso, bool insertarSeleccion);

        [OperationContract]
        List<SubZona> ObtenerSubZonas(int idTipoUsuario, int idZona, bool insertarSeleccion);

        [OperationContract]
        List<SiteRack> ObtenerSiteRacks(int idTipoUsuario, int idSubZona, bool insertarSeleccion);

        [OperationContract]
        Ubicacion ObtenerUbicacion(int idPais, int? idCampus, int? idTorre, int? idPiso, int? idZona, int? idSubZona,int? idSiteRack);

        [OperationContract]
        void GuardarUbicacion(Ubicacion ubicacion);
    }
}
