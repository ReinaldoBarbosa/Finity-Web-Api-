
namespace Finity.Api.Dtos.Usuarios;

public class UsuarioRespostaDto
{
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public int IdPerfil { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
}
