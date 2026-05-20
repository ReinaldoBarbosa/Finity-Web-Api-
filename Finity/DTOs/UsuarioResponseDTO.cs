namespace Finity.DTOs
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cargo { get; set; }

        public string NivelAcesso { get; set; }

        public int DepartamentoId { get; set; }

        public string Matricula { get; set; }
    }
}
