using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.User
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsClient { get; set; }

        public bool IsBarber { get; set; }
    }
}
