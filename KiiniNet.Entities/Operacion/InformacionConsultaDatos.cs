using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class InformacionConsultaDatos
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdInformacionConsulta { get; set; }
        
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int? Orden { get; set; }

        [DataMember]
        public int? IdPadre { get; set; }

        [DataMember]
        public virtual InformacionConsulta InformacionConsulta { get; set; }
    }
}
