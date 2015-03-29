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
    public class N_TestFood
    {
        // Declaración de variables
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public String nombre { set; get; }
        [DataMember]
        public String descripcion { set; get; }
        [DataMember]
        public Int32 tipo { set; get; }
        [DataMember]
        public String caracteristicasMonitorizadas { set; get; }


        // Constructor de la clase
        public N_TestFood()
        {
            this.id = 0;
            this.nombre = String.Empty;
            this.descripcion = String.Empty;
            this.tipo = 0;
            this.caracteristicasMonitorizadas = String.Empty;

        }
    }
}
