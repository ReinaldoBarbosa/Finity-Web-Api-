using Finity.Data;
using Finity.DTOs;
using Finity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finity.Services
{
    public class AuthService
    {
        private readonly FinityDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(FinityDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string Login(LoginDTO dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

            if(usuario == null)
                throw new Exception("Usuário não encontrado");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);

            if ( !senhaValida )
                throw new Exception("Senha inválida");

            return GerarToken(usuario);
        }


        private string GerarToken(Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.NivelAcesso)

            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
