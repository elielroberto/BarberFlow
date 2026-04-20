using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Domain.Entities
{
    public class BlockedTime
    {
        public Guid Id { get; set; }

        public Guid ProfessionalId { get; set; }
        public Professional Professional { get; set; } = null!;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
