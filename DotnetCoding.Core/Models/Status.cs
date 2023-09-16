

using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models
{
public enum StatusEnum{
    Pending = 1,
    Rejected = 2,
    Approved = 3
}

    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
