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
        public DbSet<Models.Categoria> Categorias { get; set; }
        public DbSet<Models.Assinatura> Assinaturas { get; set; }

        // (futuro)
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
                    .HasColumnName("senha_hash");

                entity.Property(u => u.Cargo)
                    .HasColumnName("cargo");

                entity.Property(u => u.NivelAcesso)
                    .HasColumnName("nivel_acesso");

                entity.Property(u => u.IdDepartamento)
                    .HasColumnName("departamento_id");

                entity.Property(u => u.StatusUsuario)
                    .HasColumnName("status_usuario");

                entity.Property(u => u.Matricula)
                    .HasColumnName("matricula");

                entity.HasOne(u => u.Departamento)
                    .WithMany(d => d.Usuarios)
                    .HasForeignKey(u => u.IdDepartamento);
            });

            modelBuilder.Entity<Assinatura>(entity =>
            {
                entity.ToTable("Assinatura");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id");

                entity.Property(u => u.Nome)
                    .HasColumnName("nome");

                entity.Property(u => u.Valor)   
                    .HasColumnName("valor");

                entity.Property(u => u.DataVencimento)
                    .HasColumnName("data_vencimento");

                entity.Property(u => u.Status)
                    .HasColumnName("status");

                entity.Property(u => u.CategoriaId)
                    .HasColumnName("id_categoria");

                entity.Property(u => u.UsuarioId)
                    .HasColumnName("id_usuario");

                entity.Property(u => u.Descricao)
                    .HasColumnName("descricao");

                entity.Property(u => u.DataCriacao)
                    .HasColumnName("data_criacao");

                entity.HasOne(a => a.Categoria)
                      .WithMany(c => c.Assinaturas)
                      .HasForeignKey(a => a.CategoriaId);

                entity.HasOne(a => a.Usuario)
                      .WithMany(u => u.Assinaturas)
                      .HasForeignKey(a => a.UsuarioId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
