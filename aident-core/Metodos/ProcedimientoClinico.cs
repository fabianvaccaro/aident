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
    
    public partial class ProcedimientoClinico
    {
        public int Id { get; set; }
        public int orden { get; set; }
        public string descripcion { get; set; }
        public int idMpat { get; set; }
    
        public virtual Mpat Mpat { get; set; }
    }
}
