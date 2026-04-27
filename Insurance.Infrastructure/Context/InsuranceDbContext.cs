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
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(14);
                entity.Property(e => e.NomeSegurado).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MarcaModeloVeiculo).IsRequired().HasMaxLength(100);

                entity.Property(e => e.ValorVeiculo).HasPrecision(18, 2);
                entity.Property(e => e.PremioComercial).HasPrecision(18, 2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
