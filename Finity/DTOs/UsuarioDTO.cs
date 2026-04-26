namespace Finity.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cargo { get; set; }
        public string NivelAcesso { get; set; }
        public int IdDepartamento { get; set; }
        public string StatusUsuario { get; set; }
        public string Matricula { get; set; }
    }
}
