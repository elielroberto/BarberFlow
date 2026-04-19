using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.Interfaces;
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
            var result = await _appointmentService.CreateAsync(dto);

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
    }
}
