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
    public class N_CiclosEvaluacion
    {
        // Declaración de variables
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public Int32 idMpat { set; get; }
        [DataMember]
        public Int32 numeroCiclos { set; get; }
        [DataMember]
        public Int32 orden { set; get; }

        // Constructor de la clase
        public N_CiclosEvaluacion()
        {
            id = 0;
            idMpat = 0;
            numeroCiclos = 0;
            orden = 0;
        }
    }
}
