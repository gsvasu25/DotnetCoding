
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models
{
    public class BaseEntity:IGuid
    {
        [Required]
        public DateTime CreatedTimeUtc { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdateTimeUtc { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid GUID { get; set; } = Guid.NewGuid();
    }
}
