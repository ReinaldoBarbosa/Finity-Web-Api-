using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Cadastro
{
    [Table("Centro_Custo")]
    public class CentroCusto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_centro_custo")]
        public int Id { get; set; }

        [Column("id_empresa")]
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("descricao")]
        public string? Descricao { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("data_cadastro", TypeName = "datetime2")]
        public DateTime DataCadastro { get; set; }
    }
}
