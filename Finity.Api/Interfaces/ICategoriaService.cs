
using Finity.Api.Dtos.Categorias;

namespace Finity.Api.Interfaces;

public interface ICategoriaService
{
    Task<List<CategoriaRespostaDto>> ListarAsync();
    Task<CategoriaRespostaDto?> BuscarPorIdAsync(int id);
    Task<CategoriaRespostaDto> CriarAsync(CriarCategoriaDto dto);
    Task<CategoriaRespostaDto?> AtualizarAsync(int id, AtualizarCategoriaDto dto);
}
