using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Cadastro
{

    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_categoria")]
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

        [Required]
        [StringLength(10)]
        [Column("tipo", TypeName = "varchar(10)")]
        public string Tipo { get; set; } = string.Empty;

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("data_cadastro", TypeName = "datetime2")]
        public DateTime DataCadastro { get; set; }


    }
}
