
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Finity.Api.Data;
using Finity.Api.Dtos.Autenticacao;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Finity.Api.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AutenticacaoService(
        AppDbContext context,
        IPasswordHasher<Usuario> passwordHasher,
        IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<LoginRespostaDto?> LoginAsync(LoginDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        var usuario = await _context.Usuarios
            .AsNoTracking()
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null ||
            !usuario.Ativo ||
            usuario.Perfil is null ||
            !usuario.Perfil.Ativo)
                {
                    return null;
                }

        var resultado = _passwordHasher.VerifyHashedPassword(
            usuario,
            usuario.SenhaHash,
            dto.Senha);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        var jwtKey = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "A chave JWT não foi configurada.");

        var key = new SymmetricSecurityKey(
            Convert.FromBase64String(jwtKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var agora = DateTime.UtcNow;
        var expiracao = agora.AddHours(1);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email),
            new(ClaimTypes.Name, usuario.Nome),
            new("id_empresa", usuario.IdEmpresa.ToString()),
            new("id_perfil", usuario.IdPerfil.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "Finity.Api",
            audience: "Finity.Client",
            claims: claims,
            notBefore: agora,
            expires: expiracao,
            signingCredentials: credentials);

        return new LoginRespostaDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiraEm = expiracao,
            IdUsuario = usuario.Id,
            IdEmpresa = usuario.IdEmpresa,
            IdPerfil = usuario.IdPerfil,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
