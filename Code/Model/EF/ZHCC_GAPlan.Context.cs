﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ZHCC_GAPlanEntities : DbContext
    {
        public ZHCC_GAPlanEntities()
            : base("name=ZHCC_GAPlanEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActualSteps> ActualSteps { get; set; }
        public virtual DbSet<BusinessCertificate> BusinessCertificate { get; set; }
        public virtual DbSet<BusinessIntroduction> BusinessIntroduction { get; set; }
        public virtual DbSet<BusinessLicense> BusinessLicense { get; set; }
        public virtual DbSet<FlightTask> FlightTask { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<RepetitivePlan> RepetitivePlan { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<TWFLibrary> TWFLibrary { get; set; }
        public virtual DbSet<TWFSteps> TWFSteps { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Pilot> Pilot { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<FlightPlan> FlightPlan { get; set; }
        public virtual DbSet<Company> Company { get; set; }
    }
}
