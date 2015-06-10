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
    public class S_Procedimiento
    {
        [DataMember]
        public Int32 Id { set; get; }
        [DataMember]
        public Int32 idMpat { set; get; }
        [DataMember]
        public String descripcion { set; get; }
        [DataMember]
        public Int32 orden { set; get; }

        public S_Procedimiento()
        {
            this.Id = 0;
            this.idMpat = 0;
            this.descripcion = String.Empty;
            this.orden = 0;
        }

        public S_Procedimiento(Int32 id, Int32 idMpat, String descripcion, Int32 orden)
        {
            this.Id = id;
            this.idMpat = idMpat;
            this.descripcion = descripcion;
            this.orden = orden;
        }
    }
}
