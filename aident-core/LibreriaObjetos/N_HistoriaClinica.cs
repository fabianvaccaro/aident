using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MainCore
{
    [DataContract]
    [Serializable]
    public class N_HistoriaClinica
    {
        [DataMember]
        public Int32 id { get; set; }
        [DataMember]
        public String odontograma { get; set; }
        [DataMember]
        public Int32 numeroCariados { get; set; }
        [DataMember]
        public Int32 numeroDientesPerdidos { get; set; }
        [DataMember]
        public Int32 numeroDientesObturados { get; set; }
        [DataMember]
        public String ortodoncia { get; set; }
        [DataMember]
        public String protesis { get; set; }
        [DataMember]
        public Int32 implantes { get; set; }
        [DataMember]
        public Int32 paresAntagPerdidos{ get; set; }
        [DataMember]
        public Int32 gradoEdentulismo { get; set; }
        [DataMember]
        public Boolean estadoSaludGeneral { get; set; }
        [DataMember]
        public Boolean enfermedadCardioVascular{ get; set; }
        [DataMember]
        public Boolean enfermedadRenal { get; set; }
        [DataMember]
        public Boolean ICTUS { get; set; }
        [DataMember]
        public Boolean ACV { get; set; }
        [DataMember]
        public Boolean paralisisFacial { get; set; }
        [DataMember]
        public Int32 gradoDesnutricion { get; set; }

        /// <summary>
        /// Constructor general de la clase N_Paciente
        /// </summary>
        public N_HistoriaClinica()
        {
            this.id = 0;
            this.odontograma = String.Empty;
            this.numeroCariados = 0;
            this.numeroDientesPerdidos = 0;
            this.numeroDientesObturados = 0;
            this.ortodoncia = String.Empty;
            this.protesis = String.Empty;
            this.implantes = 0;
            this.paresAntagPerdidos = 0;
            this.gradoEdentulismo = 0;
            this.estadoSaludGeneral = false;
            this.enfermedadCardioVascular = false;
            this.enfermedadRenal = false;
            this.ICTUS = false;
            this.ACV = false;
            this.paralisisFacial = false;
            this.gradoDesnutricion = 0;
        }
    }
}
