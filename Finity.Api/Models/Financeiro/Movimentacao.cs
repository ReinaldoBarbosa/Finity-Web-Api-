
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;

namespace Finity.Api.Models.Financeiro;

[Table("Movimentacao")]
public class Movimentacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_movimentacao")]
    public int Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    public Empresa Empresa { get; set; } = null!;

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    public Categoria Categoria { get; set; } = null!;

    [Column("id_centro_custo")]
    public int? IdCentroCusto { get; set; }

    public CentroCusto? CentroCusto { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    public Usuario Usuario { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("valor", TypeName = "decimal(19,2)")]
    public decimal Valor { get; set; }

    [Required]
    [StringLength(10)]
    [Column("tipo", TypeName = "varchar(10)")]
    public string Tipo { get; set; } = string.Empty;

    [Column("data_movimentacao", TypeName = "date")]
    public DateOnly DataMovimentacao { get; set; }

    [Column("data_cadastro", TypeName = "datetime2")]
    public DateTime DataCadastro { get; set; }

    [Column("id_movimentacao_original")]
    public int? IdMovimentacaoOriginal { get; set; }

    public Movimentacao? MovimentacaoOriginal { get; set; }

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
