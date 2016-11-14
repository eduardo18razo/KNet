using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Mascaras;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class Catalogos
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Tabla { get; set; }
        [DataMember]
        public string ComandoInsertar { get; set; }
        [DataMember]
        public string ComandoActualizar { get; set; }
        [DataMember]
        public bool EsMascaraCaptura { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<CampoMascara> CampoMascara { get; set; }
    }
}
