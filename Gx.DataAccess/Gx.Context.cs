﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Gx.Models;

namespace Gx.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GxEntities : DbContext
    {
        public GxEntities()
            : base("name=GxEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<gx_dict_config> gx_dict_config { get; set; }
        public DbSet<gx_gkxx> gx_gkxx { get; set; }
        public DbSet<gx_gxxx> gx_gxxx { get; set; }
        public DbSet<gx_LeaveOpinion> gx_LeaveOpinion { get; set; }
        public DbSet<gx_photogallery> gx_photogallery { get; set; }
        public DbSet<gx_rjxx> gx_rjxx { get; set; }
        public DbSet<gx_sys_models> gx_sys_models { get; set; }
        public DbSet<gx_sys_permissions> gx_sys_permissions { get; set; }
        public DbSet<gx_sys_role> gx_sys_role { get; set; }
        public DbSet<gx_sys_rolemenu> gx_sys_rolemenu { get; set; }
        public DbSet<gx_sys_roleperRelate> gx_sys_roleperRelate { get; set; }
        public DbSet<gx_sys_user> gx_sys_user { get; set; }
        public DbSet<gx_sys_userrolerelate> gx_sys_userrolerelate { get; set; }
        public DbSet<gx_systemlog> gx_systemlog { get; set; }
        public DbSet<gx_taskwork> gx_taskwork { get; set; }
        public DbSet<gx_wfactivityinstance> gx_wfactivityinstance { get; set; }
        public DbSet<gx_wfprocess> gx_wfprocess { get; set; }
        public DbSet<gx_wfprocessinstance> gx_wfprocessinstance { get; set; }
        public DbSet<gx_wftask> gx_wftask { get; set; }
        public DbSet<gx_wftransitionInstance> gx_wftransitionInstance { get; set; }
        public DbSet<gx_xlzx> gx_xlzx { get; set; }
        public DbSet<gx_yszlfile> gx_yszlfile { get; set; }
    }
}
