

using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models
{
    public class ProductDetails : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public bool IsActive { get; set; }
    }
}
