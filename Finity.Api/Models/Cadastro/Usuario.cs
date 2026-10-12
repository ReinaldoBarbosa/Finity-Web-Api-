using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Cadastro
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("id_empresa")]
        public int IdEmpresa { get; set; }

        [Column("id_perfil")]
        public int IdPerfil { get; set; }

        public Empresa Empresa { get; set; } = null!;
        public Perfil Perfil { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(320)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("data_cadastro", TypeName = "datetime2")]
        public DateTime DataCadastro { get; set; }
    }
}
