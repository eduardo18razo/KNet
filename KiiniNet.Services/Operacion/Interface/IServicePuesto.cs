using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServicePuesto
    {
        [OperationContract]
        List<Puesto> ObtenerPuestos(bool insertarSeleccion);

        [OperationContract]
        void Guardar(Puesto puesto);
    }
}
