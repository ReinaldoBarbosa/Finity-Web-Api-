
using Finity.Api.Dtos.Empresas;

namespace Finity.Api.Interfaces;

public interface IEmpresaService
{
    Task<List<EmpresaRespostaDto>> ListarAsync();

    Task<EmpresaRespostaDto?> BuscarPorIdAsync(int id);

    Task<EmpresaRespostaDto> CriarAsync(CriarEmpresaDto dto);

    Task<EmpresaRespostaDto?> AtualizarAsync(
        int id,
        AtualizarEmpresaDto dto);
}
