using System;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Helper
{
    public class HelperTickets
    {
        public int IdTicket { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioAsignado { get; set; }
        public DateTime FechaHora { get; set; }
        public int NumeroTicket { get; set; }
        public string NombreUsuario { get; set; }
        public string Tipificacion { get; set; }
        public string GrupoAsignado { get; set; }
        public string UsuarioAsignado { get; set; }
        public string NivelUsuarioAsignado { get; set; }
        public EstatusTicket EstatusTicket { get; set; }
        public EstatusAsignacion EstatusAsignacion { get; set; }
        public int Total { get; set; }

        public bool EsPropietario { get; set; }
    }
}
