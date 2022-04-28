using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order>? Orders { get; set; }

        public DbSet<Product>? Products { get; set; }

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
