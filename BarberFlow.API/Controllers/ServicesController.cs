using BarberFlow.Application.DTOs;
using BarberFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _service.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateServiceDto dto)
        {
            var service = await _service.UpdateAsync(id,dto);

            if (service == null)
                return NotFound();

            return Ok(service);
        }
    }
}
