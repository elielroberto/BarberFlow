using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Domain.Entities
{
    public class Client
    {
       public Guid Id { get; set; }
       public string Name { get; set; } = string.Empty;
       public string Phone { get; set; } = string.Empty;
       public int UserId { get; set; }
    }
}
