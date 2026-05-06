using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.Appointment
{
    public class AvailableSlotDto
    {
        public DateTime Start { get; set; }

        public bool Available { get; set; }

        public string? ClientName { get; set; }

        public string? ServiceName { get; set; }

        public DateTime? EndTime { get; set; }
        public bool IsBlocked { get; set; }
        public Guid? BlockedId { get; set; }
        public Guid? AppointmentId { get; set; }


    }
}
