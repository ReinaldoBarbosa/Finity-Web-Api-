
using Finity.Api.Data;
using Finity.Api.Dtos.Usuarios;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuarioService(
        AppDbContext context,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<UsuarioRespostaDto>> ListarAsync()
    {
        return await _context.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.Nome)
            .Select(u => MapearResposta(u))
            .ToListAsync();
    }

    public async Task<UsuarioRespostaDto?> BuscarPorIdAsync(int id)
    {
        var usuario = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return usuario is null ? null : MapearResposta(usuario);
    }

    public async Task<UsuarioRespostaDto> CriarAsync(CriarUsuarioDto dto)
    {
        var nome = dto.Nome.Trim();
        var email = NormalizarEmail(dto.Email);

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Senha) ||
            dto.Senha.Length < 8)
        {
            throw new ArgumentException(
                "A senha deve possuir pelo menos 8 caracteres.");
        }

        var empresa = await _context.Empresas
            .FirstOrDefaultAsync(e => e.Id == dto.IdEmpresa);

        if (empresa is null)
            throw new ArgumentException("A empresa informada não existe.");

        if (!empresa.Ativo)
            throw new InvalidOperationException(
                "Não é possível cadastrar usuário em uma empresa inativa.");

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(p => p.Id == dto.IdPerfil);

        if (perfil is null)
            throw new ArgumentException("O perfil informado não existe.");

        if (!perfil.Ativo)
            throw new InvalidOperationException(
                "Não é possível utilizar um perfil inativo.");

        var emailExiste = await _context.Usuarios
            .AnyAsync(u => u.Email == email);

        if (emailExiste)
            throw new InvalidOperationException(
                "Já existe um usuário cadastrado com esse e-mail.");

        var usuario = new Usuario
        {
            IdEmpresa = dto.IdEmpresa,
            IdPerfil = dto.IdPerfil,
            Nome = nome,
            Email = email,
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        // A senha original nunca é armazenada no banco.
        usuario.SenhaHash = _passwordHasher.HashPassword(
            usuario,
            dto.Senha);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return MapearResposta(usuario);
    }

    public async Task<UsuarioRespostaDto?> AtualizarAsync(
        int id,
        AtualizarUsuarioDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
            return null;

        var nome = dto.Nome.Trim();
        var email = NormalizarEmail(dto.Email);

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.");

        var emailExiste = await _context.Usuarios
            .AnyAsync(u => u.Id != id && u.Email == email);

        if (emailExiste)
            throw new InvalidOperationException(
                "Outro usuário já utiliza esse e-mail.");

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(p => p.Id == dto.IdPerfil);

        if (perfil is null)
            throw new ArgumentException("O perfil informado não existe.");

        if (!perfil.Ativo)
            throw new InvalidOperationException(
                "Não é possível atribuir um perfil inativo.");

        usuario.Nome = nome;
        usuario.Email = email;
        usuario.IdPerfil = dto.IdPerfil;
        usuario.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        return MapearResposta(usuario);
    }

    private static string NormalizarEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static UsuarioRespostaDto MapearResposta(Usuario usuario)
    {
        return new UsuarioRespostaDto
        {
            Id = usuario.Id,
            IdEmpresa = usuario.IdEmpresa,
            IdPerfil = usuario.IdPerfil,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Ativo = usuario.Ativo,
            DataCadastro = usuario.DataCadastro
        };
    }
}
