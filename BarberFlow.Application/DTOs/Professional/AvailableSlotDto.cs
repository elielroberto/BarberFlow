using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.Professional
{
    public class AvailableSlotDto
    {
        public DateTime Start { get; set; }
        public bool Available { get; set; }
    }
}
