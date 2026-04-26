using Finity.Models;
using Microsoft.EntityFrameworkCore;

namespace Finity.Data
{
    public class FinityDbContext : DbContext
    {
        public FinityDbContext(DbContextOptions<FinityDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Departamento> Departamentos { get; set; }

        // (futuro)
        // public DbSet<Assinatura> Assinaturas { get; set; }
        // public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id");

                entity.Property(u => u.Nome)
                    .HasColumnName("nome");

                entity.Property(u => u.Email)
                    .HasColumnName("email");

                entity.Property(u => u.SenhaHash)
                    .HasColumnName("senha");

                entity.Property(u => u.Cargo)
                    .HasColumnName("cargo");

                entity.Property(u => u.NivelAcesso)
                    .HasColumnName("nivel_acesso");

                entity.Property(u => u.IdDepartamento)
                    .HasColumnName("id_departamento");

                entity.Property(u => u.StatusUsuario)
                    .HasColumnName("statusUsuario");

                entity.Property(u => u.Matricula)
                    .HasColumnName("matricula");

                entity.HasOne(u => u.Departamento)
                    .WithMany(d => d.Usuarios)
                    .HasForeignKey(u => u.IdDepartamento);

               
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
