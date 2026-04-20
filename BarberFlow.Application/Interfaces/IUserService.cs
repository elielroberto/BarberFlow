using BarberFlow.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateUserRoleAsync(UpdateUserRoleDto dto);
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserDetailsDto?> GetByIdAsync(Guid id);
    }
}
