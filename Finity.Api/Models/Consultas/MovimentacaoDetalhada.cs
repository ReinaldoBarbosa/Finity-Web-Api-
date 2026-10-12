
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Consultas;

public class MovimentacaoDetalhada
{
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_movimentacao")]
    public int IdMovimentacao { get; set; }

    [Column("id_movimentacao_original")]
    public int? IdMovimentacaoOriginal { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("valor", TypeName = "decimal(19,2)")]
    public decimal Valor { get; set; }

    [Column("tipo", TypeName = "varchar(10)")]
    public string Tipo { get; set; } = string.Empty;

    [Column("data_movimentacao", TypeName = "date")]
    public DateOnly DataMovimentacao { get; set; }

    [Column("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [Column("centro_custo")]
    public string? CentroCusto { get; set; }

    [Column("usuario")]
    public string Usuario { get; set; } = string.Empty;

    [Column("cancelada")]
    public bool Cancelada { get; set; }

    [Column("data_cancelamento", TypeName = "datetime2")]
    public DateTime? DataCancelamento { get; set; }

    [Column("motivo_cancelamento")]
    public string? MotivoCancelamento { get; set; }

    [Column("eh_estorno")]
    public bool? EhEstorno { get; set; }
}
