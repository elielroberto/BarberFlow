using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.BlockedTime
{
    public class CreateBlockedTimeDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Reason { get; set; }
    }
}
