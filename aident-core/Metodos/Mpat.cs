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
    
    public partial class Mpat
    {
        public Mpat()
        {
            this.TestFood = new HashSet<TestFood>();
        }
    
        public int Id { get; set; }
        public int idTestFood { get; set; }
        public int ciclosMasticatorios { get; set; }
        public int ciclosEvaluacion { get; set; }
        public string procedimiento { get; set; }
        public int Estado { get; set; }
    
        public virtual ICollection<TestFood> TestFood { get; set; }
        public virtual Experimento Experimento { get; set; }
    }
}
