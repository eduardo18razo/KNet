using System.Collections.Generic;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniNet.Entities.Cat.Sistema
{
    public class TipoArbolAcceso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Habilitado { get; set; }
        public virtual List<ArbolAcceso> ArbolAcceso { get; set; }
    }
}
