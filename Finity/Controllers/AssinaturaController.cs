using Finity.DTOs;
using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly AssinaturaService _service;

        public AssinaturaController(AssinaturaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetUser();
            var assinaturas = _service.ListarAssinatura(userId);
            return Ok(assinaturas);
        }

        private int GetUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("Usuário não autenticado");
            return int.Parse(userIdClaim.Value);
        }


        [Authorize]
        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(AssinaturaDTO dto)
        {
            var userId = GetUser();

            var assinatura = _service.Criar(dto, userId);

            return Ok(assinatura);
        }

        [Authorize]
        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            var userId = GetUser();

            var assinatura = _service.Deletar(id, userId);

            return Ok("assinatura deletada com sucesso");
        }

        [Authorize]
        [HttpPut]
        [Route("atualizar/{id}")]
        public IActionResult Atualizar(int id, AssinaturaDTO dto)
        {
            var userId = GetUser();
            var assinatura = _service.Atualizar(id, dto, userId);
            return Ok(assinatura);
        }

        [Authorize]
        [HttpGet("proximas")]
        public IActionResult Proximas()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int id = int.Parse(usuarioId);

            var resultado = _service.BuscarProximas(id);

            return Ok(resultado);
        }

    }
}
