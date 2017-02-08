using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class TipoDocumento
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public List<InformacionConsulta> InformacionConsulta { get; set; }
    }
}
