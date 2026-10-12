
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Usuarios;

public class CriarUsuarioDto
{
    [Required]
    public int IdEmpresa { get; set; }

    [Required]
    public int IdPerfil { get; set; }

    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(320)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Senha { get; set; } = string.Empty;
}
