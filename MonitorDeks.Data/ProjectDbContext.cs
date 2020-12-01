using DashboardDeks.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Data
{
    public class ProjectDbContext : IdentityDbContext
    {
        public ProjectDbContext() {}

        public ProjectDbContext(DbContextOptions options): base(options) {}

        new public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Outline> Outlines { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PartnerReport> PartnerReports { get; set; }
        public virtual DbSet<ResetStatus> ResetStatuses { get; set; }

    }
}
