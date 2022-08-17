using System.ComponentModel.DataAnnotations;

namespace KarmaStore.Models
{
    public class Category_Model
    {
        [Required]
        public string Name { get; set; }
    }
}
