using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    class N_Paciente
    {
        private enum T_Sexo
        {
            NULO, HOMBRE, MUJER
        }
        
        public Int32 id { set; get; }
        public String identificacion { set; get; }
        public String nombre{ set; get;}
        public String ubicacion { set; get; }
        public T_Sexo sexo { set; get; }
        public Int32 idHistoriaClinica { set; get; }

        public N_Paciente()
        {
            this.id = 0;
            this.identificacion = String.Empty;
            this.nombre = String.Empty;
            this.ubicacion = String.Empty;
            this.sexo = T_Sexo.NULO;
            this.idHistoriaClinica = 0;
        }
    }
}
