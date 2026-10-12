
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Usuarios;

public class AtualizarUsuarioDto
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(320)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int IdPerfil { get; set; }

    public bool Ativo { get; set; }
}
