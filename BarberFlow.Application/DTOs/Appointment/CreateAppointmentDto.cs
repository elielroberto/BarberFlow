using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public Guid ProfessionalId { get; set; }
        public Guid ServiceId { get; set; }

        public DateTime StartTime { get; set; }
    }
}
