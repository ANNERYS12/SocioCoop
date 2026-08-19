using Microsoft.EntityFrameworkCore;
using SocioCoop.Domain;

namespace SocioCoop.Infraestructure.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Aporte> Aportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Socio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cedula).IsRequired().HasMaxLength(20);
                entity.Property(e => e.BalanceAportes).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Aporte>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Concepto).HasMaxLength(200);

                entity.HasOne(a => a.Socio)
                      .WithMany(s => s.Aportes)
                      .HasForeignKey(a => a.SocioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}