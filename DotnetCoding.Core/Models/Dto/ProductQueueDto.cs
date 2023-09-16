using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models.Dto
{
    public class ProductQueueDto
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }
        public double NewPrice { get; set; }
        public string UpdateType { get; set; }
        public string Guid { get; set; }
    }
}
