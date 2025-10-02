using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
    }
}