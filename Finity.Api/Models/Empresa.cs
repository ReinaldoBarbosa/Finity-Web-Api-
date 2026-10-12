
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models;

[Table("Empresa")]
public class Empresa
{
    [Key]
    [Column("id_empresa")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("razao_social")]
    public string RazaoSocial { get; set; } = string.Empty;

    [MaxLength(200)]
    [Column("nome_fantasia")]
    public string? NomeFantasia { get; set; }

    [Required]
    [StringLength(14, MinimumLength = 14)]
    [Column("cnpj", TypeName = "char(14)")]
    public string Cnpj { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(320)]
    [Column("email")]
    public string? Email { get; set; }

    [MaxLength(320)]
    [Column("telefone")]
    public string? Telefone { get; set; }

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    [Column("data_cadastro", TypeName = "datetime2")]
    public DateTime DataCadastro { get; set; }
}
