using BarberFlow.Application.DTOs.Professional;

namespace BarberFlow.Application.Interfaces
{
    public interface IProfessionalService
    {
        Task<Guid?> GetProfessionalIdByUserIdAsync(Guid userId);
        Task<List<ProfessionalResponseDto>> GetAllAsync();
    }
}