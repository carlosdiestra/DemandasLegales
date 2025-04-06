using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGP_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Infrastructure.Configurations
{
    public class FirmaConfiguration : IEntityTypeConfiguration<Firma>
    {
        public void Configure(EntityTypeBuilder<Firma> builder)
        {
            builder.ToTable("Firmas");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Rol).IsRequired().HasMaxLength(1);
            builder.Property(f => f.Parte).IsRequired();
        }
    }
}
