namespace Finity.Models
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int IdAssinatura { get; set; }

        public DateTime? DataPagamento { get; set; }

        public decimal ValorPago { get; set; }

        public string Status { get; set; }


        public string? Observacao { get; set; }

        public int MesReferencia { get; set; }

        public int AnoReferencia { get; set; }


        public Assinatura Assinatura { get; set; }
    }
}
