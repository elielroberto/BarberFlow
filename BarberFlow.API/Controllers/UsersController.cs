using BarberFlow.Application.DTOs.User;
using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("role")]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleDto dto)
        {
            var result = await _userService.UpdateUserRoleAsync(dto);

            if (!result)
                return NotFound("Usuário não encontrado");

            return Ok("Role atualizada com sucesso");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("Usuário não encontrado");

            return Ok(user);
        }
    }
}
