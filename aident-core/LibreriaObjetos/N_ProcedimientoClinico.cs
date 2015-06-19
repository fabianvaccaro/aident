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
    public class N_ProcedimientoClinico
    {
        // Declaración de variables
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public Int32 idMpat { set; get; }
        [DataMember]
        public String descripcion { set; get; }
        [DataMember]
        public Int32 orden { set; get; }

        // Constructor de la clase
        public N_ProcedimientoClinico()
        {
            this.id = 0;
            this.descripcion = String.Empty;
        }
        public S_Procedimiento toSTipo()
        {
            S_Procedimiento resultado = new S_Procedimiento();
            resultado.descripcion = this.descripcion;
            resultado.Id = this.id;
            resultado.idMpat = this.idMpat;
            resultado.orden = this.orden;
            return resultado;
        }
    }
}
