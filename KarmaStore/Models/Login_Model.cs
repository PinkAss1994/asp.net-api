using System.ComponentModel.DataAnnotations;

namespace KarmaStore.Models
{
    public class Login_Model
    {
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
