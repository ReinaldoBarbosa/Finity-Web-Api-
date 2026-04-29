namespace Finity.Models
{
    public class Assinatura
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
