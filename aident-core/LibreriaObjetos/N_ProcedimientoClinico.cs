using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    class N_ProcedimientoClinico
    {
        // Declaración de variables
        public Int32 id { set; get; }
        public String descripcion { set; get; }

        // Constructor de la clase
        public N_ProcedimientoClinico()
        {
            this.id = 0;
            this.descripcion = String.Empty;
        }
    }
}
