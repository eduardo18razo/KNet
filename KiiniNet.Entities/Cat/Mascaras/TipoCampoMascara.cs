using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace KiiniNet.Entities.Cat.Mascaras
{
    [DataContract(IsReference = true)]
    public class TipoCampoMascara
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string TipoDatoSql { get; set; }
        [DataMember]
        public bool LongitudMinima { get; set; }
        [DataMember]
        public bool LongitudMaxima { get; set; }
        [DataMember]
        public bool Entero { get; set; }
        [DataMember]
        public bool Decimal { get; set; }
        [DataMember]
        public bool ValorMaximo { get; set; }
        [DataMember]
        public bool SimboloMoneda { get; set; }
        [DataMember]
        public bool Catalogo { get; set; }
        [DataMember]
        public bool Mask { get; set; }
        [DataMember]
        public bool UploadFile { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<CampoMascara> CampoMascara { get; set; }
    }
}
