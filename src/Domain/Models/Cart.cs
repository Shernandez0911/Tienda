using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int SubTotal { get; set; }
        public string BuyerId { get; set; } = null!;
        public int? UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}