
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Api.Models.Consultas;

public class ResumoPorCategoria
{
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [Column("tipo", TypeName = "varchar(10)")]
    public string Tipo { get; set; } = string.Empty;

    [Column("total")]
    public decimal? Total { get; set; }
}
