using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }
    }
}
