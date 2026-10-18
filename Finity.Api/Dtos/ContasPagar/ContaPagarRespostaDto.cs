namespace Finity.Api.Dtos.ContasPagar;

public class ContaPagarRespostaDto
{
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public int IdCategoria { get; set; }
    public string TipoCategoria { get; set; } = string.Empty;
    public int? IdCentroCusto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public DateOnly DataEmissao { get; set; }
    public DateOnly DataVencimento { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Cancelada { get; set; }
}