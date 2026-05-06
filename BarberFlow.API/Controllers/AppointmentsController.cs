using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.DTOs.BlockedTime;
using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized("Usuário não identificado no token.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Token inválido.");

            var result = await _appointmentService.CreateAsync(userId, dto);

            if (!result)
                return BadRequest("Não foi possível agendar (conflito ou dados inválidos)");

            return Ok("Agendamento criado com sucesso");
        }

        [Authorize]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _appointmentService.CancelAsync(id);

            if (!result)
                return NotFound("Agendamento não encontrado ou já cancelado");

            return Ok("Agendamento cancelado com sucesso");
        }

        [Authorize(Roles = "Client")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized("Usuário não identificado.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Token inválido.");

            var result = await _appointmentService.GetMyAppointmentsAsync(userId);

            return Ok(result);
        }
    }
}
