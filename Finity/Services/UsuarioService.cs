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

        public async Task<bool> AtualizarUsuarioAsync(
            int id,
            UsuarioUpdateDto dto
        )
        {
            var usuario =
                await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return false;

            usuario.Nome = dto.Nome;

            usuario.Email = dto.Email;

            usuario.Cargo = dto.Cargo;

            usuario.NivelAcesso = dto.NivelAcesso;

            usuario.IdDepartamento = dto.IdDepartamento;

            usuario.StatusUsuario = dto.StatusUsuario;

            usuario.Matricula = dto.Matricula;

            // Atualiza senha somente se preenchida
            if (!string.IsNullOrEmpty(dto.Senha))
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
