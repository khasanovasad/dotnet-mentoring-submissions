using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaybeNull]
        public IEnumerable<Product> Products { get; set; }
    }
}
