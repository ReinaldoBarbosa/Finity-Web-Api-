
namespace Finity.Api.Dtos.Empresas;

public class EmpresaRespostaDto
{
    public int Id { get; set; }

    public string RazaoSocial { get; set; } = string.Empty;

    public string? NomeFantasia { get; set; }

    public string Cnpj { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }
}
