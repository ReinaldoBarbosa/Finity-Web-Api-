using Finity.Constants;
using Finity.Data;
using Finity.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Finity.Services
{
    public class DashboardService
    {
        private readonly FinityDbContext _context;

        public DashboardService(FinityDbContext context)
        {
            _context = context;
        }

        public int ObterTotalAssinaturas(int usuarioId)
        {
            var total = _context.Assinaturas.Count(a => a.UsuarioId == usuarioId);
            return total;
        }

        public int ObterPendentes(int usuarioId)
        {
            return _context.Pagamentos
                .Include(p => p.Assinatura)
                .Count(p =>
                    p.Assinatura.UsuarioId == usuarioId
                    &&
                    p.Status == StatusPagamento.Pendente
                );
        }

        public decimal ObterTotalGasto(int usuarioId)
        {
            return _context.Assinaturas
            .Where(a => a.UsuarioId == usuarioId)
            .Sum(a => (decimal?)a.Valor) ?? 0;
        }

        public decimal ObterGastoMensal(int usuarioId)
        {
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            return _context.Pagamentos
                .Include(p => p.Assinatura)
                .Where(p =>
                    p.Assinatura.UsuarioId == usuarioId
                    &&
                    p.Status == StatusPagamento.Paga
                    &&
                    p.DataPagamento.HasValue
                    &&
                    p.DataPagamento.Value.Month == mesAtual
                    &&
                    p.DataPagamento.Value.Year == anoAtual
                )
                .Sum(p => (decimal?)p.ValorPago) ?? 0;
        }

        public int ObterProximosVencimentos(int usuarioId)
        {
            var hoje = DateTime.Now.Date;
            var limite = hoje.AddDays(7);

            return _context.Assinaturas
                .Where(a => a.UsuarioId == usuarioId &&
                            a.DataVencimento >= hoje &&
                            a.DataVencimento <= limite &&
                            a.Status == StatusAssinatura.Ativa)
                .Count();
        }

        public DashboardDTO ObterDashboard(int usuarioId)
        {
            var totalAssinaturas = ObterTotalAssinaturas(usuarioId);
            var pendentes = ObterPendentes(usuarioId);
            var totalGasto = ObterTotalGasto(usuarioId);
            var gastoMensal = ObterGastoMensal(usuarioId);
            var proximosVencimentos = ObterProximosVencimentos(usuarioId);



            return new DashboardDTO
            {
                TotalAssinaturas = totalAssinaturas,
                Pendentes = pendentes,
                TotalGasto = totalGasto,
                GastoMensal = gastoMensal,
                ProximosVencimentos = proximosVencimentos,
                GastosPorCategoria = ObterGastosPorCategoria(usuarioId)
            };
        }

        public List<object> ObterGastosPorCategoria(int usuarioId)
        {
            var categorias = _context.Pagamentos
                .Include(p => p.Assinatura)
                .ThenInclude(a => a.Categoria)
                .Where(p =>
                    p.Assinatura.UsuarioId == usuarioId
                    &&
                    p.Status == StatusPagamento.Paga
                )
                .GroupBy(p => p.Assinatura.Categoria.Nome)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.ValorPago)
                })
                .OrderByDescending(g => g.Total)
                .ToList<object>();

            return categorias;
        }
    }
}
