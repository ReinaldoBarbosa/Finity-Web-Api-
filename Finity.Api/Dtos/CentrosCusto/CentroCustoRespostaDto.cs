namespace Finity.Api.Dtos.CentrosCusto;

public class CentroCustoRespostaDto
{
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
}