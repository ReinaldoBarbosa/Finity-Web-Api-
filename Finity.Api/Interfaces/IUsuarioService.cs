
using Finity.Api.Dtos.Usuarios;

namespace Finity.Api.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioRespostaDto>> ListarAsync();
    Task<UsuarioRespostaDto?> BuscarPorIdAsync(int id);
    Task<UsuarioRespostaDto> CriarAsync(CriarUsuarioDto dto);
    Task<UsuarioRespostaDto?> AtualizarAsync(
        int id,
        AtualizarUsuarioDto dto);
}
