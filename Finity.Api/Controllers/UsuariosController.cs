
using Finity.Api.Dtos.Usuarios;
using Finity.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioRespostaDto>>> Listar()
    {
        var usuarios = await _usuarioService.ListarAsync();
        return Ok(usuarios);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioRespostaDto>> BuscarPorId(int id)
    {
        var usuario = await _usuarioService.BuscarPorIdAsync(id);

        if (usuario is null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioRespostaDto>> Criar(
        CriarUsuarioDto dto)
    {
        try
        {
            var usuario = await _usuarioService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = usuario.Id },
                usuario);
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
    public async Task<ActionResult<UsuarioRespostaDto>> Atualizar(
        int id,
        AtualizarUsuarioDto dto)
    {
        try
        {
            var usuario = await _usuarioService.AtualizarAsync(id, dto);

            if (usuario is null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return Ok(usuario);
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
