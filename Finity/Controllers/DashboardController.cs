using Finity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var userId = GetUser();
                var dashboard = _dashboardService.ObterDashboard(userId);
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("não encontrada"))
                {
                    return NotFound(new { mensagem = ex.Message });
                }

                if (ex.Message.Contains("não autorizado"))
                {
                    return Unauthorized(new { mensagem = ex.Message });
                }
              
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });

            }
        }

        private int GetUser()
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
                throw new Exception("Usuário não autenticado");
            return int.Parse(userIdClaim.Value);
        }

    }
}
