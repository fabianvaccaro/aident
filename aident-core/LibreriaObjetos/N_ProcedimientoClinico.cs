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
        public String descripcion { set; get; }

        // Constructor de la clase
        public N_ProcedimientoClinico()
        {
            this.id = 0;
            this.descripcion = String.Empty;
        }
    }
}
