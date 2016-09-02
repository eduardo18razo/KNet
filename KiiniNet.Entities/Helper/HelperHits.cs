using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiiniNet.Entities.Helper
{
     [Serializable]
    public class HelperHits
    {
        public int IdHit { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaHora { get; set; }
        public int NumeroHit { get; set; }
        public string NombreUsuario { get; set; }
        public string Tipificacion { get; set; }
        public int Total { get; set; }

        
    }
}
