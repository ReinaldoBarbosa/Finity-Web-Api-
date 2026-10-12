
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Perfis;

public class AtualizarPerfilDto
{
    [Required, StringLength(80)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; }
}
