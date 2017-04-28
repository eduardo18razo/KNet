using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KiiniNet.Entities.Cat.Usuario
{
    [DataContract(IsReference = true)]
    public class Horario
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<HorarioDetalle> HorarioDetalle { get; set; }
        [DataMember]
        public virtual List<HorarioSubGrupo> HorarioSubGrupo { get; set; }
    }
}
