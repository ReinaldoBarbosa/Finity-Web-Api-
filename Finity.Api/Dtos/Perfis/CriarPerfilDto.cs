
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Perfis;

public class CriarPerfilDto
{
    [Required, StringLength(80)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Descricao { get; set; }
}
