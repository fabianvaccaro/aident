﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModeloDatosServidorContainer : DbContext
    {
        public ModeloDatosServidorContainer()
            : base("name=ModeloDatosServidorContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CiclosEvaluacionSet> CiclosEvaluacionSet { get; set; }
        public DbSet<MpatSet> MpatSet { get; set; }
        public DbSet<ProcedimientoClinicoSet> ProcedimientoClinicoSet { get; set; }
        public DbSet<UsuarioSet> UsuarioSet { get; set; }
        public DbSet<CaracteristicasExtraidasSet> CaracteristicasExtraidasSet { get; set; }
        public DbSet<MuestraSet> MuestraSet { get; set; }
        public DbSet<ExperimentoSet> ExperimentoSet { get; set; }
    }
}