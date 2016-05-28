using System.Runtime.Serialization;
using KiiniNet.Entities.Cat.Sistema;

namespace KiiniNet.Entities.Cat.Usuario
{
    [DataContract(IsReference = true)]
    public class EncuestaPregunta
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdEncuesta { get; set; }
        [DataMember]
        public string Pregunta { get; set; }
        [DataMember]
        public virtual Encuesta Encuesta { get; set; }
        [DataMember]
        public decimal Ponderacion { get; set; }
    }
}
