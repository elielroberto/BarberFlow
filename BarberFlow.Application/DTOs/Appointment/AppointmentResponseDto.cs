using BarberFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.Appointment
{
    public class AppointmentResponseDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
