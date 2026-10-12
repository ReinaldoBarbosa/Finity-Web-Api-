
using Finity.Api.Dtos.Empresas;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresasController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    // GET: api/empresas
    [HttpGet]
    public async Task<ActionResult<List<EmpresaRespostaDto>>> Listar()
    {
        var empresas = await _empresaService.ListarAsync();

        return Ok(empresas);
    }

    // GET: api/empresas/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpresaRespostaDto>> BuscarPorId(int id)
    {
        var empresa = await _empresaService.BuscarPorIdAsync(id);

        if (empresa is null)
        {
            return NotFound(new
            {
                mensagem = "Empresa não encontrada."
            });
        }

        return Ok(empresa);
    }

    // POST: api/empresas
    [HttpPost]
    public async Task<ActionResult<EmpresaRespostaDto>> Criar(
        CriarEmpresaDto dto)
    {
        try
        {
            var empresa = await _empresaService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = empresa.Id },
                empresa);
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

    // PUT: api/empresas/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmpresaRespostaDto>> Atualizar(
        int id,
        AtualizarEmpresaDto dto)
    {
        try
        {
            var empresa = await _empresaService.AtualizarAsync(id, dto);

            if (empresa is null)
            {
                return NotFound(new
                {
                    mensagem = "Empresa não encontrada."
                });
            }

            return Ok(empresa);
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
