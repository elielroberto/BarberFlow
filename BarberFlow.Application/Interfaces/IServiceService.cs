using BarberFlow.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Interfaces
{
    public interface IServiceService
    {
        public Task<Guid> CreateAsync(CreateServiceDto dto);
        public Task<List<ServiceResponseDto>> GetAllAsync();
        public Task<ServiceResponseDto> GetByIdAsync(Guid id);
        public Task<ServiceResponseDto> UpdateAsync(Guid id, UpdateServiceDto dto);
    }
}
