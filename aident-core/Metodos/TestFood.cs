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
    
    public partial class TestFood
    {
        public TestFood()
        {
            this.Mpat = new HashSet<Mpat>();
        }
    
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string caracteristicaMonitorzadas { get; set; }
        public int IdTipo { get; set; }
    
        public virtual ICollection<Mpat> Mpat { get; set; }
        public virtual TipoTestFood TipoTestFood { get; set; }
    }
}
