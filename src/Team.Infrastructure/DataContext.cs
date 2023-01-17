using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Entities;
using Team.Infrastructure.Configurations;

namespace Team.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectResourceConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectClientConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectMilestoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectClient> ProjectClients { get; set; }
        public DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public DbSet<ProjectMilestone> ProjectMilestones { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
    }
}
