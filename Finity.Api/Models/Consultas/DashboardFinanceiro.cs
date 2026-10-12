
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Consultas;

public class DashboardFinanceiro
{
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("total_receitas")]
    public decimal? TotalReceitas { get; set; }

    [Column("total_despesas")]
    public decimal? TotalDespesas { get; set; }

    [Column("saldo")]
    public decimal? Saldo { get; set; }

    [Column("quantidade_receitas")]
    public int? QuantidadeReceitas { get; set; }

    [Column("quantidade_despesas")]
    public int? QuantidadeDespesas { get; set; }
}
