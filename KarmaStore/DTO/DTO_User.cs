﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaStore.DTO
{
    [Table("User")]
    public class DTO_User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string Name { get; set; }
 
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
