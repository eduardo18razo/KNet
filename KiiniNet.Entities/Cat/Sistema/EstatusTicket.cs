﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Parametros;

namespace KiiniNet.Entities.Cat.Sistema
{
    [DataContract(IsReference = true)]
    public class EstatusTicket
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public List<Ticket> Ticket { get; set; }
        [DataMember]
        public List<EstatusSubRolGeneral> EstatusSubRolGeneral { get; set; }
    }
}
