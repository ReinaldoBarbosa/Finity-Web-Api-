namespace Finity.DTOs
{
    public class AssinaturaDetalhesDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataVencimento { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string Categoria { get; set; } = string.Empty;
    }
}
