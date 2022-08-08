using System;
using System.Collections.Generic;

namespace KarmaStore.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? RegistedAt { get; set; }
        public int? Status { get; set; }
    }
}
