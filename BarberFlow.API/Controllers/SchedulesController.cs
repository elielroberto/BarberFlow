using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public SchedulesController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(
            [FromQuery] Guid professionalId,
            [FromQuery] DateTime date)
        {
            var result = await _appointmentService.GetAvailableSlotsAsync(professionalId, date);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("day")]
        public async Task<IActionResult> GetByDay(
            [FromQuery] Guid professionalId,
            [FromQuery] DateTime date)
        {
            var result = await _appointmentService.GetByDayAsync(professionalId, date);

            return Ok(result);
        }

        [Authorize(Roles = "Professional")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMySchedule()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized("Usuário não identificado.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Token inválido.");

            var result = await _appointmentService.GetMyScheduleAsync(userId);

            return Ok(result);
        }
    }
}