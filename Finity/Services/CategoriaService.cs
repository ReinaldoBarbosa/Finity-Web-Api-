using Finity.Data;
using Finity.DTOs;
using Finity.Models;

namespace Finity.Services
{
    public class CategoriaService
    {
        private readonly FinityDbContext _context;

        public CategoriaService(FinityDbContext context)
        {
            _context = context;
        }

        public List<Models.Categoria> Listar()
        {
            return _context.Categorias.ToList();
        }

        public Categoria Criar(CategoriasDTO dto)
        {
            var categoria = new Categoria
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }


        public Categoria Atualizar(int id, CategoriasDTO dto)
        {

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            categoria.Nome = dto.Nome;
            categoria.Descricao = dto.Descricao;
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Deletar(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            var existeAssinatura = _context.Assinaturas.Any(a => a.CategoriaId == id);
            if (existeAssinatura)
                throw new Exception("Não é possível deletar categoria com assinaturas associadas");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        } 
    }
}
