//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainCore
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paciente
    {
        public int Id { get; set; }
        public int Muestra_Id { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Ubicacion { get; set; }
        public string Sexo { get; set; }
        public int IdExperimento { get; set; }
        public int idHistoriaClinica { get; set; }
    
        public virtual Muestra MuestraSet { get; set; }
        public virtual HistoriaClinica HistoriaClinica { get; set; }
        public virtual Experimento Experimento { get; set; }
    }
}
