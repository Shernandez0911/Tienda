using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public required String Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}