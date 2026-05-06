using BarberFlow.Application.DTOs.Service;

namespace BarberFlow.Application.Interfaces
{
    public interface IServiceService
    {
        Task<Guid> CreateAsync(CreateServiceDto dto);
        Task<List<ServiceResponseDto>> GetAllAsync();
        Task<ServiceResponseDto?> GetByIdAsync(Guid id);
        Task<ServiceResponseDto?> UpdateAsync(Guid id, UpdateServiceDto dto);
        Task<bool> DesactivateAsync(Guid id);
    }
}