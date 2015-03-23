using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    class N_Experimento
    {
        // Declaración de las variables
        public Int32 id { set; get; } //pk
        public String codigoExperimento { set; get; }
        public Int32 idPaciente { set; get; } //fk_N_Paciente
        public Int32 idMpat { set; get; } //fk_N_Mpat

        // Constructor de la clase
        public N_Experimento (){
            this.id = 0;
            this.codigoExperimento = String.Empty;
            this.idPaciente = 0;
            this.idMpat = 0;
 
        }
    }
}
