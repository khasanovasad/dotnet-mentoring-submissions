using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCPrinciples.DbModels
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        [StringLength(50,MinimumLength = 4)]
        [Required]
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        [Range(0,9999.9999)]
        public decimal? UnitPrice { get; set; }
        [Range(0,10000)]
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
