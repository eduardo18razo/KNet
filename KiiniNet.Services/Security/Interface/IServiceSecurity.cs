using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniNet.Services.Security.Interface
{
    [ServiceContract]
    public interface IServiceSecurity
    {
        [OperationContract]
        bool Autenticate(string user, string password);

        [OperationContract]
        Usuario GetUserDataAutenticate(string user, string password);

        [OperationContract]
        List<Menu> ObtenerMenuUsuario(int idUsuario);
    }
}
