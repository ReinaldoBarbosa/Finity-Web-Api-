using Finity.DTOs;
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

        [HttpGet]
        [Route("teste")]
        public IActionResult Teste()
        {
            return Ok("Auth funcionando");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                var token = _service.Login(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
