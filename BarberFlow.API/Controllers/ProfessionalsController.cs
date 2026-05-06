using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalsController : ControllerBase
    {
        private readonly IProfessionalService _professionalService;

        public ProfessionalsController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        [Authorize(Roles = "Professional")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfessionalId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized("Usuário não identificado no token.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Token inválido.");

            var professionalId = await _professionalService.GetProfessionalIdByUserIdAsync(userId);

            if (professionalId == null)
                return NotFound("Professional não encontrado.");

            return Ok(professionalId);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professionals = await _professionalService.GetAllAsync();

            return Ok(professionals);
        }
    }
}