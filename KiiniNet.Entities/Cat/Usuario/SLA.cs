using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Cat.Usuario
{
    [DataContract(IsReference = true)]
    public class SLA
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public decimal TiempoHoraProceso { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAcceso { get; set; }
    }
}
