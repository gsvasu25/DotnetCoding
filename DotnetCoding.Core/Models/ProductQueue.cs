

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetCoding.Core.Models
{
    public class ProductQueue : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public string? Notes {get;set;}

        [Required]
        public int UpdateTypeId { get; set; }

        [ForeignKey("UpdateTypeId")]

        public UpdateType UpdateType {get;set;}

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductDetails Product { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

    }
}
