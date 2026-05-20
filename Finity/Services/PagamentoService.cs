using Finity.Constants;
using Finity.Data;
using Finity.DTOs;
using Finity.Models;

namespace Finity.Services
{
    public class PagamentoService
    {
        private readonly FinityDbContext _context;

        public PagamentoService(FinityDbContext context)
        {
            _context = context;
        }

        public List<Pagamento> Listar()
        {
            return _context.Pagamentos.ToList();
        }

        public Pagamento Criar(CreatePagamentoDTO dto)
        {
            var assinatura = _context.Assinaturas.FirstOrDefault(a => a.Id == dto.idAssinatura);
            if (assinatura == null)
            {
                throw new Exception("Assinatura não encontrada");
            }

            var existePagamento = _context.Pagamentos.Any(p =>
                p.IdAssinatura == dto.idAssinatura &&
                p.MesReferencia == dto.mesReferencia &&
                p.AnoReferencia == dto.anoReferencia);

            if (existePagamento)
            {
                throw new Exception("Pagamento para esta assinatura e período já existe");
            }

            var pagamento = new Pagamento
            {
                IdAssinatura = dto.idAssinatura,
                DataPagamento = null,
                ValorPago = assinatura.Valor,
                Status = StatusPagamento.Pendente,
                Observacao = dto.observacao,
                MesReferencia = dto.mesReferencia,
                AnoReferencia = dto.anoReferencia
            };

            _context.Pagamentos.Add(pagamento);
            _context.SaveChanges();
            return pagamento;

        }

        public Pagamento PagarPagamento(int id)
        {
            var pagamento = _context.Pagamentos.FirstOrDefault(p => p.Id == id);
            if (pagamento == null)
            {
                throw new Exception("Pagamento não encontrado");
            }

            if (pagamento.Status == StatusPagamento.Paga)
            {
                throw new Exception("Pagamento já foi realizado");
            }

            pagamento.Status = StatusPagamento.Paga;
            pagamento.DataPagamento = DateTime.Now;
            _context.SaveChanges();
            return pagamento;
        }
    }
}
