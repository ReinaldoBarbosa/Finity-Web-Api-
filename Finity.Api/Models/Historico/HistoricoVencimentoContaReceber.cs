
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finity.Api.Models.Cadastro;
using Finity.Api.Models.Financeiro;

namespace Finity.Api.Models.Historico;

[Table("Historico_Vencimento_Conta_Receber")]
public class HistoricoVencimentoContaReceber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_historico")]
    public long Id { get; set; }

    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_conta_receber")]
    public int IdContaReceber { get; set; }

    public ContaReceber ContaReceber { get; set; } = null!;

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
