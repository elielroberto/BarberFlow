using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.DTOs.Service
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }
}
