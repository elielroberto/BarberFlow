using BarberFlow.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfessionalController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Professional")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfessionalId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim);

            var professional = await _context.Professionals
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (professional == null)
                return NotFound("Professional não encontrado");

            return Ok(professional.Id);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professionals = await _context.Professionals
                .Select(x => new
                {
                    x.Id,
                    x.Name
                })
                .ToListAsync();

            return Ok(professionals);
        }
    }
}