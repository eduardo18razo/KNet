﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        #region Mapeo
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
        public byte[] Foto { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public int Tries { get; set; }
        [DataMember]
        public DateTime? FechaBloqueo { get; set; }
        [DataMember]
        public DateTime? FechaUpdate { get; set; }
        [DataMember]
        public bool LevantaTickets { get; set; }
        [DataMember]
        public bool LevantaRecado { get; set; }

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
        public virtual List<Ticket> TicketsSolicitados { get; set; }
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
        [DataMember]
        public virtual List<UsuarioPassword> UsuarioPassword { get; set; }
        [DataMember]
        public virtual List<PreTicket> PreTicketSolicitado { get; set; }
        [DataMember]
        public virtual List<PreTicket> PreTicketLevantado { get; set; }
        [DataMember]
        public virtual List<NotaGeneral> NotaGeneral { get; set; }
        [DataMember]
        public virtual List<NotaOpcionUsuario> NotaOpcionUsuario { get; set; }
        [DataMember]
        public virtual List<NotaOpcionGrupo> NotaOpcionGrupo { get; set; }
        [DataMember]
        public virtual List<TicketConversacion> TicketConversacion { get; set; }
        [DataMember]
        public virtual List<BitacoraAcceso> BitacoraAcceso { get; set; }
        #endregion Mapeo

        public bool Supervisor { get; set; }
        public string NombreCompleto { get { return ApellidoPaterno + " " + ApellidoMaterno + " " + Nombre; } }

        public string OrganizacionCompleta { get; set; }

        public string OrganizacionFinal { get; set; }

        public string UbicacionCompleta { get; set; }

        public string UbicacionFinal { get; set; }

        public string FechaUltimoAccesoExito
        {
            get
            {
                string fecha;
                CultureInfo ci = new CultureInfo("Es-Es");
                var days = (DateTime.Now - BitacoraAcceso.Last(l => l.Success).Fecha).TotalDays;
                switch (int.Parse(Math.Abs(Math.Round(days)).ToString()))
                {
                    case 0:
                        fecha = "Hoy";
                        break;
                    case 1:
                        fecha = "Ayer";
                        break;
                    case 2:
                        fecha = ci.DateTimeFormat.GetDayName(BitacoraAcceso.Last(l => l.Success).Fecha.DayOfWeek).ToString();
                        break;
                    case 3:
                        fecha = ci.DateTimeFormat.GetDayName(BitacoraAcceso.Last(l => l.Success).Fecha.DayOfWeek).ToString();
                        break;
                    case 4:
                        fecha = ci.DateTimeFormat.GetDayName(BitacoraAcceso.Last(l => l.Success).Fecha.DayOfWeek).ToString();
                        break;
                    case 5:
                        fecha = ci.DateTimeFormat.GetDayName(BitacoraAcceso.Last(l => l.Success).Fecha.DayOfWeek).ToString();
                        break;
                    case 6:
                        fecha = ci.DateTimeFormat.GetDayName(BitacoraAcceso.Last(l => l.Success).Fecha.DayOfWeek).ToString();
                        break;
                    default:
                        fecha = BitacoraAcceso.Last(l => l.Success).Fecha.ToString("dd-MM-yy");
                        break;
                }
                return string.Format("{0} {1} hrs.", fecha, BitacoraAcceso.Last(l => l.Success).Fecha.ToString("HH:mm"));
            }
        }


    }
}
