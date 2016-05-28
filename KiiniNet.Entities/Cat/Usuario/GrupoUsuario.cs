using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniNet.Entities.Cat.Usuario
{
    [DataContract(IsReference = true)]
    public class GrupoUsuario
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoGrupo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        
        [DataMember]
        public virtual TipoGrupo TipoGrupo { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoAtendedor { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoDesarrollo { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoDueno { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoEspConsulta { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoOperador { get; set; }
        [DataMember]
        public virtual List<InventarioArbolAcceso> InventarioArbolAccesoSolicitante { get; set; }
        [DataMember]
        public virtual List<SubGrupoUsuario> SubGrupoUsuario { get; set; }
        [DataMember]
        public virtual List<UsuarioGrupo> UsuarioGrupo { get; set; }
        [DataMember]
        public virtual List<GrupoUsuarioInventarioArbol> GrupoUsuarioInventarioArbol { get; set; }
    }
}
