using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public class Image
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public required string PublicId { get; set; }
        public int ProductId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}