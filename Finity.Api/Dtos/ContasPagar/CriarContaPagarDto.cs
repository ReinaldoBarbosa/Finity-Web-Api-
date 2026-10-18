using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.ContasPagar;

public class CriarContaPagarDto
{
    [Required]
    public int IdEmpresa { get; set; }

    [Required]
    public int IdCategoria { get; set; }

    [Required]
    [StringLength(10)]
    public string TipoCategoria { get; set; } = "DESPESA";

    public int? IdCentroCusto { get; set; }

    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, 99999999999999999.99)]
    public decimal ValorOriginal { get; set; }

    public DateOnly DataEmissao { get; set; }

    public DateOnly DataVencimento { get; set; }
}