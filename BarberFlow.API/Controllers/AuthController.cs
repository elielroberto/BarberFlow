using BarberFlow.Application.DTOs.User;
using BarberFlow.Application.Interfaces;
using BarberFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result)
                return BadRequest("Email já cadastrado");

            return Ok("Usuário criado com sucesso");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Email ou senha inválidos");

            return Ok(new {token});
        }



    }
}
