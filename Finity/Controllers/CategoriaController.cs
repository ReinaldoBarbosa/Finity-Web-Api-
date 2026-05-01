using Finity.DTOs;
using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try {
                var categorias = _categoriaService.Listar();
                return Ok(categorias);
            }
            catch (Exception ex) { 
                return StatusCode(500, new { mensagem = "Erro interno"}); 
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(CategoriasDTO dto)
        {
            try
            {
                var categoria = _categoriaService.Criar(dto);
                return StatusCode(201, categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar(int id, CategoriasDTO dto)
        {
            try
            {
                var categoria = _categoriaService.Atualizar(id, dto);
                return Ok(categoria);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var categoria = _categoriaService.Deletar(id);
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
