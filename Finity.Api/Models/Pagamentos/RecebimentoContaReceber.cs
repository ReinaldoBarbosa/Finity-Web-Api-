
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;
using Finity.Api.Models.Financeiro;

namespace Finity.Api.Models.Pagamentos;

[Table("Recebimento_Conta_Receber")]
public class RecebimentoContaReceber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_recebimento")]
    public int Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_conta_receber")]
    public int IdContaReceber { get; set; }

    public ContaReceber ContaReceber { get; set; } = null!;

    [Column("id_movimentacao")]
    public int IdMovimentacao { get; set; }

    public Movimentacao Movimentacao { get; set; } = null!;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    public Usuario Usuario { get; set; } = null!;

    [Column("valor_recebido", TypeName = "decimal(19,2)")]
    public decimal ValorRecebido { get; set; }

    [Column("data_recebimento", TypeName = "date")]
    public DateOnly DataRecebimento { get; set; }

    [Column("data_cadastro", TypeName = "datetime2")]
    public DateTime DataCadastro { get; set; }
}
