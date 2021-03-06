﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Tickets;

namespace KiiniNet.Entities.Operacion.Usuarios
{
    [DataContract(IsReference = true)]
    public class Usuario
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoUsuario { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public int IdOrganizacion { get; set; }
        [DataMember]
        public string ApellidoPaterno { get; set; }
        [DataMember]
        public string ApellidoMaterno { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public bool DirectorioActivo { get; set; }
        [DataMember]
        public string NombreUsuario { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int? IdPuesto { get; set; }
        [DataMember]
        public bool Vip { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        
        [DataMember]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [DataMember]
        public virtual Ubicacion Ubicacion { get; set; }
        [DataMember]
        public virtual Organizacion Organizacion { get; set; }
        [DataMember]
        public virtual List<CorreoUsuario> CorreoUsuario { get; set; }
        [DataMember]
        public virtual List<TelefonoUsuario> TelefonoUsuario { get; set; }
        [DataMember]
        public virtual List<UsuarioRol> UsuarioRol { get; set; }
        [DataMember]
        public virtual List<UsuarioGrupo> UsuarioGrupo { get; set; }
        [DataMember]
        public virtual List<HitConsulta> HitConsulta { get; set; }
        [DataMember]
        public virtual List<Ticket> TicketsLevantados { get; set; }
        [DataMember]
        public virtual List<Ticket> TicketsResueltos { get; set; }
        [DataMember]
        public virtual List<TicketEstatus> TicketEstatus { get; set; }
        [DataMember]
        public virtual List<TicketAsignacion> TicketAsigno { get; set; }
        [DataMember]
        public virtual List<TicketAsignacion> TicketAsignado { get; set; }
        [DataMember]
        public virtual Puesto Puesto { get; set; }
        [DataMember]
        public virtual List<UsuarioLinkPassword> UsuarioLinkPassword { get; set; }
        [DataMember]
        public virtual List<SmsService> SmsService { get; set; }
        [DataMember]
        public virtual List<PreguntaReto> PreguntaReto { get; set; }

        public string NombreCompleto { get { return ApellidoPaterno + " " + ApellidoMaterno + " " + Nombre; } }

        public string OrganizacionCompleta { get; set; }

        public string OrganizacionFinal { get; set; }

        public string UbicacionCompleta { get; set; }

        public string UbicacionFinal { get; set; }

        
    }
}
