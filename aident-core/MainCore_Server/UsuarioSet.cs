//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainCore_Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsuarioSet
    {
        public UsuarioSet()
        {
            this.ExperimentoSet = new HashSet<ExperimentoSet>();
        }
    
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<ExperimentoSet> ExperimentoSet { get; set; }
    }
}
