using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class Urgencia
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public virtual List<Impacto> Impacto { get; set; }
    }
}
