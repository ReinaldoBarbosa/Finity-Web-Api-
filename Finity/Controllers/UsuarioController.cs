using Finity.DTOs;
using Finity.Models;
using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Create(UsuarioDTO dto)
        {
            var resultado = _service.Criar(dto);

            return Ok(resultado);
        }
    }
}
