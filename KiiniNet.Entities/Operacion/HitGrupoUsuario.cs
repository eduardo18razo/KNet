using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class HitGrupoUsuario
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdHit { get; set; }
        [DataMember]
        public int IdRol { get; set; }
        [DataMember]
        public int IdGrupoUsuario { get; set; }
        [DataMember]
        public int? IdSubGrupoUsuario { get; set; }
        [DataMember]
        public virtual HitConsulta HitConsulta { get; set; }
    }
}
