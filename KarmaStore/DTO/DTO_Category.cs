using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaStore.DTO
{
    [Table("Category")]
    public class DTO_Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<DTO_Products>? Products { get; set; }
    }
}
