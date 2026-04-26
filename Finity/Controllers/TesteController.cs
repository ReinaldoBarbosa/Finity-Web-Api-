using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Teste funcionando");
        }
    }
}
