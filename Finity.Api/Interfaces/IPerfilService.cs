
using Finity.Api.Dtos.Perfis;

namespace Finity.Api.Interfaces;

public interface IPerfilService
{
    Task<List<PerfilRespostaDto>> ListarAsync();
    Task<PerfilRespostaDto?> BuscarPorIdAsync(int id);
    Task<PerfilRespostaDto> CriarAsync(CriarPerfilDto dto);
    Task<PerfilRespostaDto?> AtualizarAsync(int id, AtualizarPerfilDto dto);
}
