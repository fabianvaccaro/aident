using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LibreriaObjetos
{
    [DataContract]
    [Serializable]
    public class N_Mpat
    {
        // Declaración de variables de N_Mpat
        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public String ListaProcedimientos { set; get; }
        [DataMember]
        public String nombre { set; get; }
        //public List<N_ProcedimientoClinico> ListaProcedimientos { set; get; }
        [DataMember]
        public Int32 idTestFood { set; get; }
        [DataMember]
        public Int32 CiclosMasticatorios { set; get; }
        [DataMember]
        public Int32 CiclosEvaluacion { set; get; }
        [DataMember]
        public Int32 idEstado { set; get; }       

        // Constructor de clase N_Mpat
        public N_Mpat()
        {
            this.id = 0;
            this.nombre = String.Empty;
            this.ListaProcedimientos = String.Empty;//new List<N_ProcedimientoClinico>();
            this.idTestFood = 0;
            this.CiclosMasticatorios = 0;
            this.CiclosEvaluacion = 0;
            this.idEstado = 0;
        }
        public N_Mpat(String nombre, String lp, Int32 idtf, Int32 cm, Int32 ce)
        {
            this.id = 0;
            this.nombre = nombre;
            this.ListaProcedimientos = lp;
            this.idTestFood = idtf;
            this.CiclosEvaluacion = ce;
            this.CiclosMasticatorios = cm;
            this.idEstado = 0;
        }
        
    }
}
