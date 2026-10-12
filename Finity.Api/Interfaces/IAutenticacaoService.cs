
using Finity.Api.Dtos.Autenticacao;

namespace Finity.Api.Interfaces;

public interface IAutenticacaoService
{
    Task<LoginRespostaDto?> LoginAsync(LoginDto dto);
}
