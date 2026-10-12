
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Consultas;

public class ContaPagarResumo
{
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_conta_pagar")]
    public int IdContaPagar { get; set; }

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("id_centro_custo")]
    public int? IdCentroCusto { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("valor_original", TypeName = "decimal(19,2)")]
    public decimal ValorOriginal { get; set; }

    [Column("data_emissao", TypeName = "date")]
    public DateOnly DataEmissao { get; set; }

    [Column("data_vencimento", TypeName = "date")]
    public DateOnly DataVencimento { get; set; }

    [Column("total_pago")]
    public decimal? TotalPago { get; set; }

    [Column("saldo_aberto")]
    public decimal? SaldoAberto { get; set; }

    [Column("status_conta", TypeName = "varchar(9)")]
    public string StatusConta { get; set; } = string.Empty;

    [Column("cancelada")]
    public bool Cancelada { get; set; }
}
