using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion;

namespace KiiniNet.Entities.Cat.Usuario
{
     [DataContract(IsReference = true)]
    public class Encuesta
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoEncuesta { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool EsPonderacion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual TipoEncuesta TipoEncuesta { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAcceso { get; set; }
        [DataMember]
        public virtual List<EncuestaPregunta> EncuestaPregunta { get; set; }
        [DataMember]
        public virtual List<Ticket> Ticket { get; set; }

         [DataMember]
        public virtual List<RespuestaEncuesta> RespuestaEncuesta { get; set; }
    }
}
