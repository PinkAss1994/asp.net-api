using System;
using System.Collections.Generic;

namespace KarmaStore.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
