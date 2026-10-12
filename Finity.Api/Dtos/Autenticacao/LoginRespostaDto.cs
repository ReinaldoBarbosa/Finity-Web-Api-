
namespace Finity.Api.Dtos.Autenticacao;

public class LoginRespostaDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEm { get; set; }

    public int IdUsuario { get; set; }
    public int IdEmpresa { get; set; }
    public int IdPerfil { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
