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
    
    public partial class ExperimentoSet
    {
        public ExperimentoSet()
        {
            this.MuestraSet = new HashSet<MuestraSet>();
        }
    
        public int Id { get; set; }
        public int idUsuario { get; set; }
        public int idMpat { get; set; }
    
        public virtual UsuarioSet UsuarioSet { get; set; }
        public virtual ICollection<MuestraSet> MuestraSet { get; set; }
        public virtual MpatSet MpatSet { get; set; }
    }
}
