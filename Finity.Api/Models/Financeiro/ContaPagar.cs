
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;

namespace Finity.Api.Models.Financeiro;

[Table("Conta_Pagar")]
public class ContaPagar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_conta_pagar")]
    public int Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    public Empresa Empresa { get; set; } = null!;

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    public Categoria Categoria { get; set; } = null!;

    [Required]
    [StringLength(10)]
    [Column("tipo_categoria", TypeName = "varchar(10)")]
    public string TipoCategoria { get; set; } = "DESPESA";

    [Column("id_centro_custo")]
    public int? IdCentroCusto { get; set; }

    public CentroCusto? CentroCusto { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("valor_original", TypeName = "decimal(19,2)")]
    public decimal ValorOriginal { get; set; }

    [Column("data_emissao", TypeName = "date")]
    public DateOnly DataEmissao { get; set; }

    [Column("data_vencimento", TypeName = "date")]
    public DateOnly DataVencimento { get; set; }

    [Column("data_cadastro", TypeName = "datetime2")]
    public DateTime DataCadastro { get; set; }

    [Column("cancelada")]
    public bool Cancelada { get; set; }

    [Column("data_cancelamento", TypeName = "datetime2")]
    public DateTime? DataCancelamento { get; set; }

    [Column("id_usuario_cancelamento")]
    public int? IdUsuarioCancelamento { get; set; }

    public Usuario? UsuarioCancelamento { get; set; }

    [MaxLength(500)]
    [Column("motivo_cancelamento")]
    public string? MotivoCancelamento { get; set; }
}
