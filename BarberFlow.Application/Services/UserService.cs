using BarberFlow.Application.DTOs.User;
using BarberFlow.Application.Interfaces;
using BarberFlow.Domain.Entities;
using BarberFlow.Domain.Enums;
using BarberFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateUserRoleAsync(UpdateUserRoleDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);

            if (user == null)
                return false;

            user.Role = dto.Role;

            if (dto.Role == UserRole.Professional)
            {
                var exists = await _context.Professionals
                    .AnyAsync(x => x.UserId == user.Id);

                if (!exists)
                {
                    var professional = new Professional
                    {
                        Id = Guid.NewGuid(),
                        Name = user.Email, 
                        UserId = user.Id
                    };

                    _context.Professionals.Add(professional);
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(x => new UserResponseDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Role = x.Role.ToString()
                })
                .ToListAsync();
        }

        public async Task<UserDetailsDto?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Where(x => x.Id == id)
                .Select(x => new UserDetailsDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Role = x.Role.ToString(),

                    IsClient = _context.Clients.Any(c => c.UserId == x.Id),
                    IsBarber = _context.Professionals.Any(p => p.UserId == x.Id)
                })
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
