using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouser.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Manufacturer Manufacturer { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool IsCategory { get; set; }
    }

    [Table("Categories")]
    public class OldCategory
    {
        [Key]
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
