using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniNet.Entities.Operacion.Tickets
{
    [DataContract(IsReference = true)]
    public class Ticket
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoUsuario { get; set; }
        [DataMember]
        public int IdTipoArbolAcceso { get; set; }
        [DataMember]
        public int IdArbolAcceso { get; set; }
        [DataMember]
        public int IdImpacto { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdOrganizacion { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public int? IdMascara{get;set;}
        [DataMember]
        public int? IdEncuesta { get; set; }
        [DataMember]
        public int? IdSlaEstimadoTicket { get; set; }
        [DataMember]
        public int? IdRespuestaEncuesta { get; set; }
        [DataMember]
        public int IdEstatusTicket { get; set; }
        [DataMember]
        public int IdEstatusAsignacion { get; set; }
        [DataMember]
        public DateTime FechaHoraAlta { get; set; }
        [DataMember]
        public DateTime FechaHoraFinProceso { get; set; }
        [DataMember]
        public bool Random { get; set; }
        [DataMember]
        public string ClaveRegistro { get; set; }


        [DataMember]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [DataMember]
        public virtual TipoArbolAcceso TipoArbolAcceso { get; set; }
        [DataMember]
        public virtual ArbolAcceso ArbolAcceso { get; set; }
        [DataMember]
        public virtual Impacto Impacto { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
        [DataMember]
        public virtual Organizacion Organizacion { get; set; }
        [DataMember]
        public virtual Ubicacion Ubicacion { get; set; }
        [DataMember]
        public virtual Mascara Mascara { get; set; }
        [DataMember]
        public virtual Encuesta Encuesta { get; set; }
        [DataMember]
        public virtual SlaEstimadoTicket SlaEstimadoTicket { get; set; }
        [DataMember]
        public virtual List<RespuestaEncuesta> RespuestaEncuesta { get; set; }
        [DataMember]
        public virtual EstatusTicket EstatusTicket { get; set; }
        [DataMember]
        public virtual EstatusAsignacion EstatusAsignacion { get; set; }

        [DataMember]
        public virtual List<TicketGrupoUsuario> TicketGrupoUsuario { get; set; }
        [DataMember]
        public virtual List<TicketEstatus> TicketEstatus { get; set; }
        [DataMember]
        public virtual List<TicketAsignacion> TicketAsignacion { get; set; }

    }
}
