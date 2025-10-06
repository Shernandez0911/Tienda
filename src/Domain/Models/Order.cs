using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required int Total { get; set; }
        public required int SubTotal { get; set; }
        public required int UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}