using System;
using System.Runtime.Serialization;

namespace KiiniNet.Entities.Operacion.Tickets
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
