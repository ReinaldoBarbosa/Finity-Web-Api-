
using Microsoft.EntityFrameworkCore;
using Finity.Api.Models.Cadastro;
using Finity.Api.Models.Financeiro;
using Finity.Api.Models.Pagamentos;
using Finity.Api.Models.Historico;

namespace Finity.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Cadastro
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CentroCusto> CentrosCusto { get; set; }

        // Financeiro
        public DbSet<ContaPagar> ContasPagar { get; set; }
        public DbSet<ContaReceber> ContasReceber { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        // Pagamentos
        public DbSet<PagamentoContaPagar> PagamentosContaPagar { get; set; }
        public DbSet<RecebimentoContaReceber> RecebimentosContaReceber { get; set; }

        // Histórico
        public DbSet<HistoricoVencimentoContaPagar>
            HistoricoVencimentosContaPagar
        { get; set; }

        public DbSet<HistoricoVencimentoContaReceber>
            HistoricoVencimentosContaReceber
        { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Chaves alternativas para os relacionamentos compostos.
            modelBuilder.Entity<Usuario>()
                .HasAlternateKey(u => new { u.IdEmpresa, u.Id });

            modelBuilder.Entity<Categoria>()
                .HasAlternateKey(c => new { c.IdEmpresa, c.Id, c.Tipo });

            modelBuilder.Entity<CentroCusto>()
                .HasAlternateKey(c => new { c.IdEmpresa, c.Id });

            modelBuilder.Entity<ContaPagar>()
                .HasAlternateKey(c => new { c.IdEmpresa, c.Id });

            modelBuilder.Entity<ContaReceber>()
                .HasAlternateKey(c => new { c.IdEmpresa, c.Id });

            modelBuilder.Entity<Movimentacao>()
                .HasAlternateKey(m => new { m.IdEmpresa, m.Id });

            // Usuario
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empresa)
                .WithMany()
                .HasForeignKey(u => u.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.IdPerfil)
                .OnDelete(DeleteBehavior.NoAction);

            // Categoria e Centro de Custo
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CentroCusto>()
                .HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            // Conta a pagar
            modelBuilder.Entity<ContaPagar>()
                .HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaPagar>()
                .HasOne(c => c.Categoria)
                .WithMany()
                .HasForeignKey(c => new
                {
                    c.IdEmpresa,
                    c.IdCategoria,
                    c.TipoCategoria
                })
                .HasPrincipalKey(c => new
                {
                    c.IdEmpresa,
                    c.Id,
                    c.Tipo
                })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaPagar>()
                .HasOne(c => c.CentroCusto)
                .WithMany()
                .HasForeignKey(c => new { c.IdEmpresa, c.IdCentroCusto })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaPagar>()
                .HasOne(c => c.UsuarioCancelamento)
                .WithMany()
                .HasForeignKey(c => new { c.IdEmpresa, c.IdUsuarioCancelamento })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Conta a receber
            modelBuilder.Entity<ContaReceber>()
                .HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaReceber>()
                .HasOne(c => c.Categoria)
                .WithMany()
                .HasForeignKey(c => new
                {
                    c.IdEmpresa,
                    c.IdCategoria,
                    c.TipoCategoria
                })
                .HasPrincipalKey(c => new
                {
                    c.IdEmpresa,
                    c.Id,
                    c.Tipo
                })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaReceber>()
                .HasOne(c => c.CentroCusto)
                .WithMany()
                .HasForeignKey(c => new { c.IdEmpresa, c.IdCentroCusto })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContaReceber>()
                .HasOne(c => c.UsuarioCancelamento)
                .WithMany()
                .HasForeignKey(c => new { c.IdEmpresa, c.IdUsuarioCancelamento })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Movimentação
            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Empresa)
                .WithMany()
                .HasForeignKey(m => m.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Categoria)
                .WithMany()
                .HasForeignKey(m => new { m.IdEmpresa, m.IdCategoria, m.Tipo })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id, c.Tipo })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.CentroCusto)
                .WithMany()
                .HasForeignKey(m => new { m.IdEmpresa, m.IdCentroCusto })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => new { m.IdEmpresa, m.IdUsuario })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.UsuarioCancelamento)
                .WithMany()
                .HasForeignKey(m => new { m.IdEmpresa, m.IdUsuarioCancelamento })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.MovimentacaoOriginal)
                .WithMany()
                .HasForeignKey(m => new { m.IdEmpresa, m.IdMovimentacaoOriginal })
                .HasPrincipalKey(m => new { m.IdEmpresa, m.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Pagamento de conta a pagar
            modelBuilder.Entity<PagamentoContaPagar>()
                .HasOne(p => p.ContaPagar)
                .WithMany()
                .HasForeignKey(p => new { p.IdEmpresa, p.IdContaPagar })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PagamentoContaPagar>()
                .HasOne(p => p.Movimentacao)
                .WithMany()
                .HasForeignKey(p => new { p.IdEmpresa, p.IdMovimentacao })
                .HasPrincipalKey(m => new { m.IdEmpresa, m.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PagamentoContaPagar>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => new { p.IdEmpresa, p.IdUsuario })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Recebimento de conta a receber
            modelBuilder.Entity<RecebimentoContaReceber>()
                .HasOne(r => r.ContaReceber)
                .WithMany()
                .HasForeignKey(r => new { r.IdEmpresa, r.IdContaReceber })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecebimentoContaReceber>()
                .HasOne(r => r.Movimentacao)
                .WithMany()
                .HasForeignKey(r => new { r.IdEmpresa, r.IdMovimentacao })
                .HasPrincipalKey(m => new { m.IdEmpresa, m.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecebimentoContaReceber>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => new { r.IdEmpresa, r.IdUsuario })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Histórico de vencimento: contas a pagar
            modelBuilder.Entity<HistoricoVencimentoContaPagar>()
                .HasOne(h => h.ContaPagar)
                .WithMany()
                .HasForeignKey(h => new { h.IdEmpresa, h.IdContaPagar })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistoricoVencimentoContaPagar>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => new { h.IdEmpresa, h.IdUsuario })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);

            // Histórico de vencimento: contas a receber
            modelBuilder.Entity<HistoricoVencimentoContaReceber>()
                .HasOne(h => h.ContaReceber)
                .WithMany()
                .HasForeignKey(h => new { h.IdEmpresa, h.IdContaReceber })
                .HasPrincipalKey(c => new { c.IdEmpresa, c.Id })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistoricoVencimentoContaReceber>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => new { h.IdEmpresa, h.IdUsuario })
                .HasPrincipalKey(u => new { u.IdEmpresa, u.Id })
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
