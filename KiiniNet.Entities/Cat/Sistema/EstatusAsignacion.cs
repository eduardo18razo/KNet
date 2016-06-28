using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Tickets;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class EstatusAsignacion
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public List<Ticket> Ticket { get; set; }
        [DataMember]
        public virtual List<TicketAsignacion> TicketAsignacion { get; set; }
    }
}
