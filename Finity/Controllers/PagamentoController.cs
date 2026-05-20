using Finity.DTOs;
using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _service;

        public PagamentoController(PagamentoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var pagamentos = _service.Listar();

                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(CreatePagamentoDTO dto)
        {
            try
            {
                var pagamento = _service.Criar(dto);
                return StatusCode(201, new
                {
                    pagamento.Id,
                    pagamento.Status,
                    pagamento.ValorPago,
                    pagamento.DataPagamento
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("não encontrada"))
                {
                    return NotFound(new { mensagem = ex.Message });
                }

                return BadRequest(new
                {
                    mensagem = ex.Message,
                    erro = ex.InnerException?.Message
                });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/pagar")]
        public IActionResult Pagar(int id)
        {
            try
            {
                var pagamento = _service.PagarPagamento(id);

                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("não encontrado"))
                {
                    return NotFound(new { mensagem = ex.Message });
                }

                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
