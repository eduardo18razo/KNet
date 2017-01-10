﻿using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Parametros
{
    [DataContract(IsReference = true)]
    public class EstatusAsignacionSubRolGeneralDefault
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdRol { get; set; }
        [DataMember]
        public int? IdSubRol { get; set; }
        [DataMember]
        public int IdEstatusAsignacionActual { get; set; }
        [DataMember]
        public int IdEstatusAsignacionAccion { get; set; }
        [DataMember]
        public bool ComentarioObligado { get; set; }
        [DataMember]
        public bool TieneSupervisor { get; set; }
        [DataMember]
        public bool Propietario { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        [DataMember]
        public EstatusAsignacion EstatusAsignacionActual { get; set; }
        [DataMember]
        public EstatusAsignacion EstatusAsignacionAccion { get; set; }
        [DataMember]
        public Rol Rol { get; set; }
        [DataMember]
        public SubRol SubRol { get; set; }
    }
}
