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
    
    public partial class Experimento
    {
        public Experimento()
        {
            this.Paciente = new HashSet<Paciente>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int NumeroPacientes { get; set; }
        public int idMpat { get; set; }
    
        public virtual ICollection<Paciente> Paciente { get; set; }
        public virtual Mpat Mpat { get; set; }
    }
}
