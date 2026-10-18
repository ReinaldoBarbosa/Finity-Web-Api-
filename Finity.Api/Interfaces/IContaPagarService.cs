using Finity.Api.Dtos.ContasPagar;

namespace Finity.Api.Interfaces;

public interface IContaPagarService
{
    Task<List<ContaPagarRespostaDto>> ListarAsync();
    Task<ContaPagarRespostaDto?> BuscarPorIdAsync(int id);
    Task<ContaPagarRespostaDto> CriarAsync(CriarContaPagarDto dto);
    Task<ContaPagarRespostaDto?> AtualizarAsync(
        int id, AtualizarContaPagarDto dto);
}