using System;
using System.Runtime.Serialization;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class InformacionConsultaDocumento
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdInformacionConsulta { get; set; }
        [DataMember]
        public string Archivo { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public virtual InformacionConsulta InformacionConsulta { get; set; }
    }
}
