using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class HitConsulta
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdArbolAcceso { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public int IdOrganizacion { get; set; }
        [DataMember]
        public virtual List<HitGrupoUsuario> HitGrupoUsuario { get; set; }
        [DataMember]
        public virtual ArbolAcceso ArbolAcceso { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
        [DataMember]
        public virtual Organizacion Organizacion { get; set; }
        [DataMember]
        public virtual Ubicacion Ubicacion { get; set; }
    }
}
