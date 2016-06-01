using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Entities.Cat.Usuario
{
    [DataContract(IsReference = true)]
    public class Sla
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public decimal TiempoHoraProceso { get; set; }
        [DataMember]
        public bool Detallado { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAcceso { get; set; }

        [DataMember]
        public virtual List<SlaDetalle> SlaDetalle { get; set; }
    }
}
