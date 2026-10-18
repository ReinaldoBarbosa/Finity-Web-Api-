using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.CentrosCusto;

public class CriarCentroCustoDto
{
    [Required]
    public int IdEmpresa { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Descricao { get; set; }
}