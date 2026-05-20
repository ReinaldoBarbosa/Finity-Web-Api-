using Finity.Data;
using Finity.DTOs;
using Finity.Models;

namespace Finity.Services
{
    public class DepartamentoService
    {
        private readonly FinityDbContext _context;

        public DepartamentoService(FinityDbContext context)
        {
            _context = context;
        }

        public List<Departamento> Listar()
        {
            return _context.Departamentos.ToList();
        }

        public Departamento Criar(DepartamentoDTO dto)
        {
            var departamento = new Departamento
            {
                nome = dto.Nome,
                descricao = dto.Descricao
            };

            _context.Departamentos.Add(departamento);
            _context.SaveChanges();
            return departamento;
        }

        public Departamento Atualizar(int id, DepartamentoDTO dto)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.id == id);
            if (departamento == null)
                throw new Exception("Departamento não encontrado");

            departamento.nome = dto.Nome;
            departamento.descricao = dto.Descricao;
            _context.SaveChanges();
            return departamento;
        }

        public Departamento Deletar(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento == null)
                throw new Exception("Departamento não encontrado");

            var existeUsuario = _context.Usuarios.Any(u => u.IdDepartamento == id);
            if (existeUsuario)
                throw new Exception("Não é possível deletar departamento com usuários associados");

            _context.Departamentos.Remove(departamento);
            _context.SaveChanges();
            return departamento;
        }
    }
}
