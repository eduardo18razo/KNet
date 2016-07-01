using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Helper
{
    public class HelperDetalleTicket
    {
        public int IdTicket { get; set; }
        public int IdEstatusTicket { get; set; }
        public int IdEstatusAsignacion { get; set; }
        public string EstatusActual { get; set; }
        public string AsignacionActual { get; set; }
        public DateTime FechaCreacion { get; set; }

        public List<HelperEstatusDetalle> EstatusDetalle { get; set; }
        public List<HelperAsignacionesDetalle> AsignacionesDetalle { get; set; }
    }

    public class HelperAsignacionesDetalle
    {
        public string Descripcion { get; set; }
        public string UsuarioAsigno { get; set; }
        public string UsuarioAsignado { get; set; }
        public DateTime FechaMovimiento { get; set; }

    }

    public class HelperEstatusDetalle
    {
        public string Descripcion { get; set; }
        public string UsuarioMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
