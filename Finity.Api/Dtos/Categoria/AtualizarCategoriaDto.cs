
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Categorias;

public class AtualizarCategoriaDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [StringLength(10)]
    public string Tipo { get; set; } = string.Empty;

    public bool Ativo { get; set; }
}
