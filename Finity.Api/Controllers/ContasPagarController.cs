using Finity.Api.Dtos.ContasPagar;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/contas-pagar")]
public class ContasPagarController : ControllerBase
{
    private readonly IContaPagarService _service;

    public ContasPagarController(IContaPagarService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContaPagarRespostaDto>>> Listar()
    {
        return Ok(await _service.ListarAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContaPagarRespostaDto>> BuscarPorId(int id)
    {
        var conta = await _service.BuscarPorIdAsync(id);

        if (conta is null)
            return NotFound(new { mensagem = "Conta a pagar não encontrada." });

        return Ok(conta);
    }

    [HttpPost]
    public async Task<ActionResult<ContaPagarRespostaDto>> Criar(
        CriarContaPagarDto dto)
    {
        try
        {
            var conta = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = conta.Id },
                conta);
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
    public async Task<ActionResult<ContaPagarRespostaDto>> Atualizar(
        int id, AtualizarContaPagarDto dto)
    {
        try
        {
            var conta = await _service.AtualizarAsync(id, dto);

            if (conta is null)
                return NotFound(new { mensagem = "Conta a pagar não encontrada." });

            return Ok(conta);
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