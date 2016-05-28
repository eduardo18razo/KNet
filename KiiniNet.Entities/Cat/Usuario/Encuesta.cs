using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Cat.Usuario
{
    public class Encuesta
    {
        public int Id { get; set; }
        public int IdTipoEncuesta { get; set; }
        public string Descripcion { get; set; }
        public bool EsPonderacion { get; set; }
        public bool Habilitado { get; set; }
        public virtual TipoEncuesta TipoEncuesta { get; set; }
        public virtual List<InventarioArbolAcceso> InventarioArbolAcceso { get; set; }
        public virtual List<EncuestaPregunta> EncuestaPregunta { get; set; }
    }
}
