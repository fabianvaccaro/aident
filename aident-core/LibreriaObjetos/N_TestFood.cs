using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    public class N_TestFood
    {
        // Declaración de variables
        public Int32 id { set; get; }
        public String nombre {set; get;}
        public String descripcion { set; get; }
        public Int32 tipo { set; get; }
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
