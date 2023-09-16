

using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models
{

    public enum UpdateTypeEnum{
        MoreThan5000Price = 1,
        Delete=2,
        MoreThan50Percent=3,
        MoreThan10000Price=4
    }


    public class UpdateType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
