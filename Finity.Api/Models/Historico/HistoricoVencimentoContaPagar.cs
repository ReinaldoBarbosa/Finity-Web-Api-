
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;
using Finity.Api.Models.Financeiro;

namespace Finity.Api.Models.Historico;

[Table("Historico_Vencimento_Conta_Pagar")]
public class HistoricoVencimentoContaPagar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_historico")]
    public long Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_conta_pagar")]
    public int IdContaPagar { get; set; }

    public ContaPagar ContaPagar { get; set; } = null!;

    [Column("vencimento_anterior", TypeName = "date")]
    public DateOnly VencimentoAnterior { get; set; }

    [Column("vencimento_novo", TypeName = "date")]
    public DateOnly VencimentoNovo { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    public Usuario Usuario { get; set; } = null!;

    [Column("data_alteracao", TypeName = "datetime2")]
    public DateTime DataAlteracao { get; set; }
}
