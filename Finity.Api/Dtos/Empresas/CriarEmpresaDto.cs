
using System.ComponentModel.DataAnnotations;

namespace Finity.Api.Dtos.Empresas;

public class CriarEmpresaDto
{
    [Required]
    [StringLength(200)]
    public string RazaoSocial { get; set; } = string.Empty;

    [StringLength(200)]
    public string? NomeFantasia { get; set; }

    [Required]
    public string Cnpj { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(320)]
    public string? Email { get; set; }

    [StringLength(30)]
    public string? Telefone { get; set; }
}
