using Finity.Constants;
using Finity.Data;
using Finity.DTOs;

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
            var total = _context.Assinaturas.Count(a => a.UsuarioId == usuarioId && a.Status == StatusAssinatura.Pendente);
            return total;
        }

        public decimal ObterTotalGasto(int usuarioId)
        {
            var total = _context.Assinaturas.Where(a => a.UsuarioId == usuarioId && a.Status == StatusAssinatura.Ativa).Sum(a => (decimal?)a.Valor) ?? 0;
            return total;
        }

        public decimal ObterGastoMensal(int usuarioId)
        {
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            return _context.Assinaturas
                .Where(a => a.UsuarioId == usuarioId &&
                            a.DataVencimento.Month == mesAtual &&
                            a.DataVencimento.Year == anoAtual &&
                            a.Status == StatusAssinatura.Ativa)
                .Sum(a => (decimal?)a.Valor) ?? 0;
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
                ProximosVencimentos = proximosVencimentos
            };
        }
    }
}
