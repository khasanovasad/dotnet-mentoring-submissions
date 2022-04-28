using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Abstractions;
using ORMFundamentals.Data;
using ORMFundamentals.DTO;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.EFCore
{
    public class EFProductsRepository : IProductRepositroy
    {
        private readonly ApplicationContext context;

        public EFProductsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProductAsync(AddProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Height = dto.Height,
                Weight = dto.Weight,
                Width = dto.Width,
                Length = dto.Length,
            };

            await context.Products!.AddAsync(product);

            await context.SaveChangesAsync();

            var result = await context.Products!.LastAsync();

            return result;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products!.SingleOrDefaultAsync(x => x.Id == id);

            if (product is null)
                throw new KeyNotFoundException();

            context.Products!.Remove(product);

            await context.SaveChangesAsync();

            await context.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await context.Products!.SingleOrDefaultAsync(x => x.Id == id);

            if (product is null)
                throw new KeyNotFoundException();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products!.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(int id, UpdateProductDTO dto)
        {
            var product = await context.Products!.SingleOrDefaultAsync(x => x.Id == id);
            if (product is null)
                throw new KeyNotFoundException();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Length = dto.Length;
            product.Weight = dto.Weight;
            product.Height = dto.Height;
            product.Width = dto.Width;

            context.Products!.Update(product);

            await context.SaveChangesAsync();

            var result = await context.Products!.SingleAsync(x => x.Id == id);

            return result;
        }
    }
}
