using BarberFlow.Application.DTOs.BlockedTime;
using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/blocked-times")]
    public class BlockedTimesController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public BlockedTimesController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Professional")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlockedTimeDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized("Usuário não identificado.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Token inválido.");

            var result = await _appointmentService.CreateBlockedTimeAsync(userId, dto);

            if (!result)
                return BadRequest("Não foi possível bloquear horário");

            return Ok("Horário bloqueado");
        }

        [Authorize(Roles = "Professional")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _appointmentService.RemoveBlockedTimeAsync(id);

            if (!result)
                return NotFound("Bloqueio não encontrado");

            return Ok("Bloqueio removido");
        }
    }
}