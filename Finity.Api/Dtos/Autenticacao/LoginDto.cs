
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Autenticacao;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [StringLength(320)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}
