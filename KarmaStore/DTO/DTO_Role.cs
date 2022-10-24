using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaStore.DTO
{
    [Table("Role")]
    public class DTO_Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
