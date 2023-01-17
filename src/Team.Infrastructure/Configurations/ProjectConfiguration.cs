using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Entities;

namespace Team.Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(name: "Projects");

            builder.HasKey(x => x.Id)
                .HasName("PK_Projects");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Detail)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(500)");

            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");
        }
    }
}
