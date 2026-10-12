
using Finity.Api.Dtos.Autenticacao;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/autenticacao")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AutenticacaoController(
        IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginRespostaDto>> Login(
        [FromBody] LoginDto dto)
    {
        var resposta = await _autenticacaoService.LoginAsync(dto);

        if (resposta is null)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos, ou usuário inativo."
            });
        }

        return Ok(resposta);
    }
}
