using Insurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastructure.Context
{
    public class InsuranceDbContext:DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }

        public DbSet<Seguro> Seguros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seguro>(entity =>
            {
                entity.Property(s => s.ValorVeiculo).HasPrecision(18, 2);
                entity.Property(s => s.TaxaRisco).HasPrecision(18, 4); // Taxas podem ter mais casas
                entity.Property(s => s.PremioRisco).HasPrecision(18, 2);
                entity.Property(s => s.PremioPuro).HasPrecision(18, 2);
                entity.Property(s => s.PremioComercial).HasPrecision(18, 2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
