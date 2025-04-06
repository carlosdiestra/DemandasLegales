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
    public class HistorialJuicioConfiguration : IEntityTypeConfiguration<HistorialJuicio>
    {
        public void Configure(EntityTypeBuilder<HistorialJuicio> builder)
        {
            builder.ToTable("HistorialJuicio");
            builder.HasKey(h => h.Id);
            builder.Property(h => h.ParteDemandante).IsRequired();
            builder.Property(h => h.ParteDemandado).IsRequired();
            builder.Property(h => h.Ganador).IsRequired();
        }
    }
}
