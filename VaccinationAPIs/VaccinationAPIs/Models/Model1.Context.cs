﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VaccinationAPIs.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class vaccinesystemEntities : DbContext
    {
        public vaccinesystemEntities()
            : base("name=vaccinesystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChildInfo> ChildInfoes { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Muhalla> Muhallas { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Schedual> Scheduals { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UnionCouncil> UnionCouncils { get; set; }
        public virtual DbSet<Vaccinator> Vaccinators { get; set; }
        public virtual DbSet<VaccinatorTask> VaccinatorTasks { get; set; }
        public virtual DbSet<tblPolio> tblPolios { get; set; }
    }
}
