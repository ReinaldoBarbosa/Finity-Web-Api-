using Finity.Data;
using Finity.DTOs;
using Finity.Models;

namespace Finity.Services
{
    public class AssinaturaService
    {
        private readonly FinityDbContext _context;

        public AssinaturaService(FinityDbContext context)
        {
            _context = context;
        }

        public List<Assinatura> ListarAssinatura(int usuarioId)
        {
            return _context.Assinaturas.Where(a => a.UsuarioId == usuarioId).ToList();
        }

        public Assinatura Deletar(int id, int usuarioId)
        {
            var assinatura = _context.Assinaturas.FirstOrDefault(a => a.Id == id && a.UsuarioId == usuarioId);
            if (assinatura == null)
                throw new Exception("Assinatura não encontrada");
            _context.Assinaturas.Remove(assinatura);
            _context.SaveChanges();
            return assinatura;
        }
        

        public Assinatura Criar(AssinaturaDTO assinaturaDTO, int usuarioId)
        {
            var assinatura = new Assinatura
            {
                Nome = assinaturaDTO.Nome,
                Valor = assinaturaDTO.Valor,
                DataVencimento = assinaturaDTO.DataVencimento,
                Status = assinaturaDTO.Status,
                CategoriaId = assinaturaDTO.CategoriaId,
                UsuarioId = usuarioId,
                Descricao = assinaturaDTO.Descricao,
                DataCriacao = DateTime.Now
            };

            _context.Assinaturas.Add(assinatura);
            _context.SaveChanges();

            return assinatura;
        }

        public Assinatura Atualizar(int id, AssinaturaDTO assinaturaDTO, int usuarioId)
        {
            var assinatura = _context.Assinaturas.FirstOrDefault(a => a.Id == id && a.UsuarioId == usuarioId);
            if (assinatura == null)
                throw new Exception("Assinatura não encontrada");

            assinatura.Nome = assinaturaDTO.Nome;
            assinatura.Valor = assinaturaDTO.Valor;
            assinatura.DataVencimento = assinaturaDTO.DataVencimento;
            assinatura.Status = assinaturaDTO.Status;
            assinatura.CategoriaId = assinaturaDTO.CategoriaId;
            assinatura.Descricao = assinaturaDTO.Descricao;
            
            _context.SaveChanges();

            return assinatura;
        }
    }
}