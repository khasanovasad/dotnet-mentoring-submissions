using ORMFundamentals.DTO;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Abstractions
{
    public interface IProductRepositroy
    {
        public Task<IEnumerable<Product>> GetProductsAsync();

        public Task<Product> GetProductAsync(int id);

        public Task DeleteProductAsync(int id);

        public Task<Product> AddProductAsync(AddProductDTO dto);

        public Task<Product> UpdateProductAsync(int id, UpdateProductDTO dto);
    }
}
