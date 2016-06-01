using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class RespuestaEncuesta
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTicket { get; set; }
        [DataMember]
        public int IdEncuesta { get; set; }
        [DataMember]
        public int IdPregunta { get; set; }
        [DataMember]
        public decimal Ponderacion { get; set; }
        [DataMember]
        public virtual Ticket Ticket { get; set; }

        [DataMember]
        public virtual Encuesta Encuesta { get; set; }
        [DataMember]
        public virtual EncuestaPregunta EncuestaPregunta { get; set; }
    }
}
