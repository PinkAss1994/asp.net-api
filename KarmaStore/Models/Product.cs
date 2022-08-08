using System;
using System.Collections.Generic;

namespace KarmaStore.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public string? Color { get; set; }
        public string? Details { get; set; }
        public string? Description { get; set; }
        public string? Images { get; set; }
        public double? Sale { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
