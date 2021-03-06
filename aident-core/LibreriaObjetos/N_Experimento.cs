﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    [DataContract]
    [Serializable]
    public class N_Experimento
    {
        // Declaración de las variables
        [DataMember]
        public Int32 id { set; get; } //pk
        [DataMember]
        public String codigoExperimento { set; get; }
        [DataMember]
        public Int32 idPaciente { set; get; } //fk_N_Paciente OJOOOOOO!!!!!!!!!!
        [DataMember]
        public Int32 idMpat { set; get; } //fk_N_Mpat
        [DataMember]
        public Int32 numeroPacientes { set; get; } //fk_N_Mpat
        [DataMember]
        public Boolean procesado { set; get; } //Estado del experimento

        // Constructor de la clase
        public N_Experimento (){
            this.id = 0;
            this.codigoExperimento = String.Empty;
            this.idPaciente = 0;
            this.idMpat = 0;
            this.numeroPacientes = 0;
            this.procesado = false; // falso = no procesado
        }
    }
}
