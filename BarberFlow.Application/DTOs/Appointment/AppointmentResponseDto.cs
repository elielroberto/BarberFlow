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
        public string ClientName { get; set; } = string.Empty;
        public string ProfessionalName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
