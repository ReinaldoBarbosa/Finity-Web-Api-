using Finity.DTOs;
using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoService _departamentoService;

        public DepartamentoController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var departamentos = _departamentoService.Listar();
                return Ok(departamentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno" });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(DepartamentoDTO dto)
        {
            try
            {
                var departamento = _departamentoService.Criar(dto);
                return StatusCode(201, departamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

         [Authorize]
         [HttpPut]
         [Route("{id}")]
         public IActionResult Atualizar(int id, DepartamentoDTO dto)
         {
             try
             {
                 var departamento = _departamentoService.Atualizar(id, dto);
                 return Ok(departamento);
             }
             catch (Exception ex)
             {
                 return BadRequest(new { mensagem = ex.Message });
             }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _departamentoService.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("não encontrada"))
                {
                    return NotFound(new { mensagem = ex.Message });
                }

                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
