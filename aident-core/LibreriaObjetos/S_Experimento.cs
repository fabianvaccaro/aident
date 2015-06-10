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
    public class S_Experimento
    {
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public Int32 idMpat{ set; get; }
        [DataMember]
        public Int32 idUsuario{ set; get; }

        public S_Experimento()
        {
            this.id = 0;
            this.idMpat = 0;
            this.idUsuario = 0;
        }

        public S_Experimento(Int32 id, Int32 idMpat, Int32 idUsuario)
        {
            this.id = id;
            this.idMpat = idMpat;
            this.idUsuario = idUsuario;
        }
    }

}
