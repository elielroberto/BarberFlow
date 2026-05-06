using BarberFlow.Application.DTOs.Professional;
using BarberFlow.Application.Interfaces;
using BarberFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberFlow.Application.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly AppDbContext _context;

        public ProfessionalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetProfessionalIdByUserIdAsync(Guid userId)
        {
            var professional = await _context.Professionals
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (professional == null)
                return null;

            return professional.Id;
        }

        public async Task<List<ProfessionalResponseDto>> GetAllAsync()
        {
            return await _context.Professionals
                .AsNoTracking()
                .Select(x => new ProfessionalResponseDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}