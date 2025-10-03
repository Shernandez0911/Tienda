using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public required int Quantity { get; set; }
        public required int PriceAtMoment { get; set; }
        public required string TitleAtMoment { get; set; }
        public required string DescriptionAtMoment { get; set; }
        public required string ImageAtMoment { get; set; }
        public int DiscountAtMoment { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

    }
}