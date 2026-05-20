namespace Finity.DTOs
{
    public class PagamentoDTO
    {
        public int Id { get; set; }
        public int IdAssinatura { get; set; }
        public decimal ValorPago { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observacao { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public string NomeAssinatura { get; set; }
    }
}
