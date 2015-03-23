using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    class N_Mpat
    {
        // Declaración de variables de N_Mpat
        public Int32 id { set; get;}
        public List<N_ProcedimientoClinico> ListaProcedimientos { set; get; }
        public Int32 idTestFood { set; get;}
        public Int32 CiclosMasticatorios { set; get;}
        public Int32 CiclosEvaluacion { set; get; }

        // Constructor de clase N_Mpat
        public N_Mpat()
        {
            this.id = 0;
            this.ListaProcedimientos = new List<N_ProcedimientoClinico>();
            this.TestFood = 0;
            this.CiclosMasticatorios = 0;
            this.CiclosEvaluacion = 0;
        }
        
    }
}
