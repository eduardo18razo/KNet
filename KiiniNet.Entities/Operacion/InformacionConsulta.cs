using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Operacion
{
    [DataContract(IsReference = true)]
    public class InformacionConsulta
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoInfConsulta { get; set; }
        [DataMember]
        public int? IdTipoDocumento { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        
        [DataMember]
        public virtual TipoInfConsulta TipoInfConsulta { get; set; }

        [DataMember]
        public virtual TipoDocumento TipoDocumento { get; set; }

        [DataMember]
        public virtual List<InformacionConsultaDatos> InformacionConsultaDatos { get; set; }
        [DataMember]
        public virtual List<InventarioInfConsulta> InventarioInfConsulta { get; set; }
        [DataMember]
        public virtual List<InformacionConsultaDocumento> InformacionConsultaDocumento { get; set; }
        
    }
}
