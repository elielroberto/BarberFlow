using BarberFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.User
{
    public class UpdateUserRoleDto
    {
        public Guid UserId { get; set; }
        public UserRole Role { get; set; } 
    }
}
