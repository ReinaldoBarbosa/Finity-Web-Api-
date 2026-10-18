using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.CentrosCusto;

public class AtualizarCentroCustoDto
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; }
}