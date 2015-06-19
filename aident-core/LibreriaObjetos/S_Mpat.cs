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
    public class S_Mpat
    {        
        // Declaración de variables de S_Mpat
        [DataMember]
        public Int32 Id { set; get; }

        [DataMember]
        public String nombre { set; get; }
        [DataMember]
        public Int32 ciclosMasticatorios { set; get; }
        [DataMember]
        public Int32 idEstado { set; get; }       
        [DataMember]
        public String DescripcionTestFood { set; get; }
        [DataMember]
        public Int32 idUsuario { set; get; }     

        [DataMember]
        public List<S_Procedimiento> ListaProcedimientos { set; get; }        
        [DataMember]
        public List<S_CiclosEvaluacion> CiclosEvaluacion { set; get; }     
   
        // Constructor de clase N_Mpat
        public S_Mpat()
        {
            this.Id = 0;
            this.nombre = String.Empty;
            this.ciclosMasticatorios = 0;
            this.idEstado = 0; 
            this.DescripcionTestFood = String.Empty;
            this.idUsuario = 0; 

            this.CiclosEvaluacion = new List<S_CiclosEvaluacion>();           
            this.ListaProcedimientos = new List<S_Procedimiento>();
        }
    }
}
