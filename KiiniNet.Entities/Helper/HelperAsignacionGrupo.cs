﻿using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Entities.Helper
{
    [Serializable]
    public class HelperAsignacionRol
    {
        public int IdRol { get; set; }
        public string DescripcionRol { get; set; }
        public List<HelperAsignacionGrupoUsuarios> Grupos { get; set; } 
        
    }

    [Serializable]
    public class HelperAsignacionGrupoUsuarios
    {
        public int IdGrupo { get; set; }
        public string DescripcionGrupo { get; set; }
        public List<HelperSubGurpoUsuario> SubGrupos { get; set; }
    }
}