using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class Menu
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int? IdPadre { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<Menu> Menu1 { get; set; }
        [DataMember]
        public virtual Menu Menu2 { get; set; }
        [DataMember]
        public virtual List<RolMenu> RolMenu { get; set; }
    }
}
