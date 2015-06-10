using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    [DataContract]
    [Serializable]
    public class S_Caracteristica
    {
        [DataMember]
        public Int32 Id { set; get; }
        [DataMember]
        public Int32 IdMuestra { set; get; }
        [DataMember]
        public Int32 CodigoCaracteristica { set; get; }
        [DataMember]
        public Double ValorCaracteristica { set; get; }

        public S_Caracteristica()
        {
            this.Id = 0;
            this.IdMuestra = 0;
            this.CodigoCaracteristica = 0;
            this.ValorCaracteristica = 0.0;
        }
        public S_Caracteristica(Int32 id, Int32 idMuestra, Int32 CodCaracteristica, Double valorCaracteristica)
        {
            this.Id = id;
            this.IdMuestra = idMuestra;
            this.CodigoCaracteristica = CodCaracteristica;
            this.ValorCaracteristica = valorCaracteristica;
        }
    }
}
