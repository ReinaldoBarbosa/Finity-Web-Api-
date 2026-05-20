namespace Finity.DTOs
{
    public class CreatePagamentoDTO
    {
        public int idAssinatura { get; set; }
        public string? observacao { get; set; }
        public int mesReferencia { get; set; }
        public int anoReferencia { get; set; }
    }
}
