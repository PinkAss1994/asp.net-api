using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaStore.DTO
{
    [Table("Products")]
    public class DTO_Products
    {
        [Key]
        public Guid ProductID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public  string Description {get; set;}

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public string color { get; set; }

        public string? Images { get; set; }

        public double Sale { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CategoryID")]
        public DTO_Category? Category { get; set; }


    }
}
