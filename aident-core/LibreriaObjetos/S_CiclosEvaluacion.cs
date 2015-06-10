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
    public class S_CiclosEvaluacion
    {
        [DataMember]
        public Int32 Id { set; get; }
        [DataMember]
        public Int32 idMpat { set; get; }
        [DataMember]
        public Int32 numeroCiclos { set; get; }
        [DataMember]
        public Int32 orden { set; get; }

        public S_CiclosEvaluacion()
        {
            this.Id = 0;
            this.idMpat = 0;
            this.numeroCiclos = 0;
            this.orden = 0;
        }

        public S_CiclosEvaluacion(Int32 id, Int32 idMpat, Int32 numCiclos, Int32 orden)
        {
            this.Id = id;
            this.idMpat = idMpat;
            this.numeroCiclos = numCiclos;
            this.orden = orden;
        }
    }
}
