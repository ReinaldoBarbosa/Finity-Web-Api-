using Finity.DTOs;
using Finity.Models;
using Finity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                var resultado = _service.Login(dto);
                 return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}

