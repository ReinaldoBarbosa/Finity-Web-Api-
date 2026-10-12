
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Consultas;

public class ResumoFinanceiro
{
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("data_movimentacao", TypeName = "date")]
    public DateOnly DataMovimentacao { get; set; }

    [Column("receitas")]
    public decimal? Receitas { get; set; }

    [Column("despesas")]
    public decimal? Despesas { get; set; }

    [Column("saldo")]
    public decimal? Saldo { get; set; }
}
