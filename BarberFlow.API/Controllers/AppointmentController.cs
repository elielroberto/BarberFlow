using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.DTOs.BlockedTime;
using BarberFlow.Application.Interfaces;
using BarberFlow.Application.Services;
using BarberFlow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
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

            return CreatedAtAction(
                nameof(GetAvailable),
                new { professionalId = dto.ProfessionalId, date = dto.StartTime.Date },
                null);
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
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _appointmentService.CancelAsync(id);

            if (!result)
                return NotFound("Agendamento não encontrado ou já cancelado");

            return Ok("Agendamento cancelado com sucesso");
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


    [Authorize(Roles = "Professional")]
    [HttpGet("barber/me")]
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

        [Authorize(Roles = "Barber")]
        [HttpPost("block")]
        public async Task<IActionResult> BlockTime(CreateBlockedTimeDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _appointmentService.CreateBlockedTimeAsync(userId, dto);

            if (!result)
                return BadRequest("Não foi possível bloquear horário");

            return Ok("Horário bloqueado");
        }
    }
}
