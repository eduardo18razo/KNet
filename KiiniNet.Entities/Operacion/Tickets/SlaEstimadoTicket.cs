using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KiiniNet.Entities.Operacion.Tickets
{
    [DataContract(IsReference = true)]
    public class SlaEstimadoTicket
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTicket { get; set; }
        [DataMember]
        public DateTime FechaInicio { get; set; }
        [DataMember]
        public DateTime FechaFin { get; set; }
        [DataMember]
        public DateTime? FechaInicioProceso { get; set; }
        [DataMember]
        public DateTime? FechaFinProceso { get; set; }
        [DataMember]
        public decimal TiempoHoraProceso { get; set; }
        [DataMember]
        public decimal TiempoRetenido { get; set; }
        [DataMember]
        public decimal TiempoDesface { get; set; }
        [DataMember]
        public bool Terminado { get; set; }
        [DataMember]
        public virtual List<Ticket> Ticket { get; set; }
        [DataMember]
        public virtual List<SlaEstimadoTicketDetalle> SlaEstimadoTicketDetalle { get; set; }
        
    }
}
