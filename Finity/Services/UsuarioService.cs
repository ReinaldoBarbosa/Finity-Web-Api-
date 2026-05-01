using Finity.Data;
using Finity.DTOs;
using Finity.Models;

namespace Finity.Services
{
    public class UsuarioService
    {
        private readonly FinityDbContext _context;

        public UsuarioService(FinityDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario Criar(UsuarioDTO dto)
        {
            Console.WriteLine(dto.Senha);

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Cargo = dto.Cargo,
                NivelAcesso = dto.NivelAcesso,
                IdDepartamento = dto.IdDepartamento,
                StatusUsuario = dto.StatusUsuario,
                Matricula = dto.Matricula
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

    }
}
