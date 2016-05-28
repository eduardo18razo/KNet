﻿using System.Collections.Generic;
using System.ServiceModel;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;

namespace KiiniNet.Services.Operacion.Interface
{
    [ServiceContract]
    public interface IServiceSubGrupoUsuario
    {
        [OperationContract]
        List<HelperSubGurpoUsuario> ObtenerSubGruposUsuario(int idGrupoUsuario, bool insertarSeleccion);

        [OperationContract]
        SubGrupoUsuario ObtenerSubGrupoUsuario(int idGrupoUsuario, int idSubRol);
    }
}
