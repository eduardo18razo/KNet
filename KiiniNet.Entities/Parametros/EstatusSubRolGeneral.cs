using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniNet.Entities.Parametros
{
    [DataContract(IsReference = true)]
    public class EstatusSubRolGeneral
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdGrupoUsuario { get; set; }
        [DataMember]
        public int IdSubRol { get; set; }
        [DataMember]
        public int IdEstatusTicket { get; set; }
        [DataMember]
        public int Orden { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public EstatusTicket EstatusTicket { get; set; }
        [DataMember]
        public GrupoUsuario GrupoUsuario { get; set; }
        [DataMember]
        public SubRol SubRol { get; set; }
        
    }
}
