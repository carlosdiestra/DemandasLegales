using Microsoft.EntityFrameworkCore;
using SGP_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            base.OnModelCreating(modelBuilder);

            // Obtiene el ensamblado actual o especifica uno en particular
            var assemblies = Assembly.GetExecutingAssembly();

            // Busca todas las clases que implementen IEntityTypeConfiguration<T>
            var configurationTypes = assemblies.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var configType in configurationTypes)
            {
                // Crear una instancia de la configuración
                var configInstance = Activator.CreateInstance(configType);

                // Obtener el tipo de entidad al que está configurando
                var entityType = configType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    .GenericTypeArguments[0];

                // Registrar la configuración en el ModelBuilder
                modelBuilder.ApplyConfiguration((dynamic)configInstance);
            }
        }
    }
}
