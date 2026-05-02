namespace Finity.DTOs
{
    public class DashboardDTO
    {
        public decimal TotalGasto { get; set; }
        public decimal GastoMensal { get; set; }
        public int Pendentes { get; set; }
        public int TotalAssinaturas { get; set; }
        public int ProximosVencimentos { get; set; }
    }
}
