using Finity.Api.Dtos.CentrosCusto;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/centros-custo")]
public class CentrosCustoController : ControllerBase
{
    private readonly ICentroCustoService _service;

    public CentrosCustoController(ICentroCustoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CentroCustoRespostaDto>>> Listar()
    {
        return Ok(await _service.ListarAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CentroCustoRespostaDto>> BuscarPorId(int id)
    {
        var centro = await _service.BuscarPorIdAsync(id);

        if (centro is null)
            return NotFound();

        return Ok(centro);
    }

    [HttpPost]
    public async Task<ActionResult<CentroCustoRespostaDto>> Criar(
        CriarCentroCustoDto dto)
    {
        try
        {
            var centro = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = centro.Id },
                centro);
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
    public async Task<ActionResult<CentroCustoRespostaDto>> Atualizar(
        int id, AtualizarCentroCustoDto dto)
    {
        try
        {
            var centro = await _service.AtualizarAsync(id, dto);

            if (centro is null)
                return NotFound();

            return Ok(centro);
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