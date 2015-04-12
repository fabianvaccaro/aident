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
    public class N_Paciente
    {
        //public enum T_Sexo
        //{
        //    NULO, HOMBRE, MUJER
        //}

        [DataMember]
        public Int32 id { set; get; }
        [DataMember]
        public String identificacion { set; get; }
        [DataMember]
        public String nombre { set; get; }
        [DataMember]
        public String ubicacion { set; get; }
        [DataMember]
        public String sexo { set; get; }
        [DataMember]
        public Int32 idHistoriaClinica { set; get; }

        public N_Paciente()
        {
            this.id = 0;
            this.identificacion = String.Empty;
            this.nombre = String.Empty;
            this.ubicacion = String.Empty;
            this.sexo = String.Empty;
            this.idHistoriaClinica = 0;
        }
    }
}
