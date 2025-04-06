using Microsoft.EntityFrameworkCore;
using SGP_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<Firma> Firmas => Set<Firma>();
        public DbSet<HistorialJuicio> Historiales => Set<HistorialJuicio>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contratos");
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.Firmas).WithOne().HasForeignKey(f => f.ContratoId);
            });

            modelBuilder.Entity<Firma>(entity =>
            {
                entity.ToTable("Firmas");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Rol).IsRequired().HasMaxLength(1);
                entity.Property(f => f.Parte).IsRequired();
            });

            modelBuilder.Entity<HistorialJuicio>(entity =>
            {
                entity.ToTable("HistorialJuicio");
                entity.HasKey(h => h.Id);
                entity.Property(h => h.ParteDemandante).IsRequired();
                entity.Property(h => h.ParteDemandado).IsRequired();
                entity.Property(h => h.Ganador).IsRequired();
            });
        }
    }
}
