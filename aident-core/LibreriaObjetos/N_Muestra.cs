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
    class N_Muestra
    {
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public Int32 ciclosMasticatorios { set; get; }
        [DataMember]
        public Int32 idPaciente { set; get; }

        public N_Muestra() {

            this.id = 0;
            this.ciclosMasticatorios = 0;
            this.idPaciente = 0;
        }
    }
}
