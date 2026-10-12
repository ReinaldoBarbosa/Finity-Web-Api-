
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;
using Finity.Api.Models.Financeiro;

namespace Finity.Api.Models.Pagamentos;

[Table("Pagamento_Conta_Pagar")]
public class PagamentoContaPagar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_pagamento")]
    public int Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_conta_pagar")]
    public int IdContaPagar { get; set; }

    public ContaPagar ContaPagar { get; set; } = null!;

    [Column("id_movimentacao")]
    public int IdMovimentacao { get; set; }

    public Movimentacao Movimentacao { get; set; } = null!;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    public Usuario Usuario { get; set; } = null!;

    [Column("valor_pago", TypeName = "decimal(19,2)")]
    public decimal ValorPago { get; set; }

    [Column("data_pagamento", TypeName = "date")]
    public DateOnly DataPagamento { get; set; }

    [Column("data_cadastro", TypeName = "datetime2")]
    public DateTime DataCadastro { get; set; }
}
