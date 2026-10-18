
using Finity.Api.Dtos.Categorias;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _service.ListarAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var categoria = await _service.BuscarPorIdAsync(id);

        return categoria is null ? NotFound() : Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarCategoriaDto dto)
    {
        try
        {
            var categoria = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = categoria.Id },
                categoria);
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
    public async Task<IActionResult> Atualizar(
        int id, AtualizarCategoriaDto dto)
    {
        try
        {
            var categoria = await _service.AtualizarAsync(id, dto);

            return categoria is null ? NotFound() : Ok(categoria);
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
