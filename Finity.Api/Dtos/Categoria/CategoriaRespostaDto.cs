
namespace Finity.Api.Dtos.Categorias;

public class CategoriaRespostaDto
{
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
}
