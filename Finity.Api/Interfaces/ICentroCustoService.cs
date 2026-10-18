using Finity.Api.Dtos.CentrosCusto;

namespace Finity.Api.Interfaces;

public interface ICentroCustoService
{
    Task<List<CentroCustoRespostaDto>> ListarAsync();
    Task<CentroCustoRespostaDto?> BuscarPorIdAsync(int id);
    Task<CentroCustoRespostaDto> CriarAsync(CriarCentroCustoDto dto);
    Task<CentroCustoRespostaDto?> AtualizarAsync(
        int id, AtualizarCentroCustoDto dto);
}