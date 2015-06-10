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
    public class S_Muestra
    {
        [DataMember]
        public Int32 nCiclos { get; set; }
        [DataMember]
        public Int32 idPaciente { get; set; }
        [DataMember]
        public Int32 idExperimento { get; set; }
        [DataMember]
        public String lado { set; get; }
        [DataMember]
        public FeatureImage vectorCaracteristicas { get; set; }

        public S_Muestra()
        {
            this.nCiclos = 0;
            this.idPaciente = 0;
            this.idExperimento = 0;
            this.lado = String.Empty;
            this.vectorCaracteristicas = new FeatureImage();

        }
        public S_Muestra(Int32 numciclos, Int32 idpac, Int32 idexperim, String codlado, FeatureImage vectorCarac)
        {
            this.nCiclos = numciclos;
            this.idPaciente = idpac;
            this.idExperimento = idexperim;
            this.lado = codlado;
            this.vectorCaracteristicas = vectorCarac;
        }
    }
}
