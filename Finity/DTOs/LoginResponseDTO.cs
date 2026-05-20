namespace Finity.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public UsuarioResponseDTO Usuario { get; set; }
    }
}
