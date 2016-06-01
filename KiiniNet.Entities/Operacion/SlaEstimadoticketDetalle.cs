using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class SlaEstimadoTicketDetalle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdSlaEstimadoTicket { get; set; }
        [DataMember]
        public int IdSubRol { get; set; }
        [DataMember]
        public decimal TiempoProceso { get; set; }
        [DataMember]
        public DateTime? HoraInicio { get; set; }
        [DataMember]
        public DateTime? HoraFin { get; set; }
        [DataMember]
        public virtual SlaEstimadoTicket SlaEstimadoTicket { get; set; }
    }
}
