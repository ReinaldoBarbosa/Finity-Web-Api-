
using Finity.Api.Dtos.Perfis;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/perfis")]
public class PerfisController : ControllerBase
{
    private readonly IPerfilService _perfilService;

    public PerfisController(IPerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PerfilRespostaDto>>> Listar()
    {
        var perfis = await _perfilService.ListarAsync();
        return Ok(perfis);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PerfilRespostaDto>> BuscarPorId(int id)
    {
        var perfil = await _perfilService.BuscarPorIdAsync(id);

        if (perfil is null)
            return NotFound(new { mensagem = "Perfil não encontrado." });

        return Ok(perfil);
    }

    [HttpPost]
    public async Task<ActionResult<PerfilRespostaDto>> Criar(
        CriarPerfilDto dto)
    {
        try
        {
            var perfil = await _perfilService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = perfil.Id },
                perfil);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PerfilRespostaDto>> Atualizar(
        int id,
        AtualizarPerfilDto dto)
    {
        try
        {
            var perfil = await _perfilService.AtualizarAsync(id, dto);

            if (perfil is null)
                return NotFound(new { mensagem = "Perfil não encontrado." });

            return Ok(perfil);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }
}
