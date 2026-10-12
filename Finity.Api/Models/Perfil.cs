using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_perfil")]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("descricao")]
        public string? Descricao { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }
    }
}
