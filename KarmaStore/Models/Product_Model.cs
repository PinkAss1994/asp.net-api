using KarmaStore.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaStore.Models
{
    public class Product_Model
    {
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public string color { get; set; }

        public double Sale { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public IFormFile Images { get; set; }
    }
}
