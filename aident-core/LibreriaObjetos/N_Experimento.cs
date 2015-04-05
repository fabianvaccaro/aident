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
        // Constructor de la clase
        public N_Experimento (){
            this.id = 0;
            this.codigoExperimento = String.Empty;
            this.idPaciente = 0;
            this.idMpat = 0;
            this.numeroPacientes = 0;
        }

        public N_Experimento(String cExperimento, Int32 iMpat, Int32 nPacientes)
        {
            // TODO: Complete member initialization
            this.id = 0;
            this.codigoExperimento = cExperimento;
            this.idPaciente = 0;
            this.idMpat = iMpat;
            this.numeroPacientes = nPacientes;
        }
    }
}
