﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KiiniNet.Entities.Operacion.Usuarios
{
    public class PreguntaReto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public string Pregunta { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
    }
}