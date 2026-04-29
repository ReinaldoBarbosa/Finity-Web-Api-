using Finity.Models;

namespace Finity.DTOs
{
    public class AssinaturaDTO
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
