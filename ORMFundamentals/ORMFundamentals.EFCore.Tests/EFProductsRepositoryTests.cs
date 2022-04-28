using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Data;
using ORMFundamentals.DTO;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ORMFundamentals.EFCore.Tests
{
    public class EFProductsRepositoryTests
    {
        private readonly EFProductsRepository repository;
        private readonly ApplicationContext context;

        public EFProductsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDataBase")
                .Options;

            context = new ApplicationContext(options);
            repository = new(context);
        }

        [Fact]
        public async Task AddProduct_For2NewProduct_ShouldIncreaseCountOfProductsBy2()
        {
            await repository.AddProductAsync(new() { Name = "Product1" });
            await repository.AddProductAsync(new() { Name = "Product2" });

            var result = (await repository.GetProductsAsync()).Count();

            result.Should().Be(2);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task AddProduct_ForNewProduct_ShouldAddNewProduct()
        {
            var result = await repository.AddProductAsync(new() { Name = "Product1" });

            var product = await repository.GetProductAsync(result.Id);

            result.Name.Should().Be(product.Name);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task DeleteProduct_ForOnlyExistingId_ShouldMakeCountOfProductsEqual0()
        {
            var product = await repository.AddProductAsync(new() { Name = "Product" });

            await repository.DeleteProductAsync(product.Id);

            var result = (await repository.GetProductsAsync()).Count();

            result.Should().Be(0);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task DeleteProduct_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            var func = async () => await repository.DeleteProductAsync(0);

            await func.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetProduct_ForExistingId_ShouldReturnProduct()
        {
            var product = repository.AddProductAsync(new() { Name = "Product" });

            var result = await repository.GetProductAsync(product.Id);

            result.Id.Should().Be(product.Id);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetProduct_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            var func = async () => await repository.GetProductAsync(0);

            await func.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetProducts_For2Products_ShouldReturn2Products()
        {
            await repository.AddProductAsync(new() { Name = "Product1" }); 
            await repository.AddProductAsync(new() { Name = "Product2" });

            var result = (await repository.GetProductsAsync()).Count();

            result.Should().Be(2);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UpdateProduct_ForNewDescription_ShouldUpdateDescription()
        {
            var description = "New description";

            var product = await repository.AddProductAsync(new() { Name = "Product" });

            var dto = new UpdateProductDTO()
            {
                Height = product.Height,
                Length = product.Length,
                Name = product.Name,
                Weight = product.Weight,
                Width = product.Width,
                Description = description,
            };

            var result = await repository.UpdateProductAsync(product.Id, dto);

            result.Description.Should().Be(description);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UpdateProduct_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            var func = async () => await repository.UpdateProductAsync(0, new());

            await func.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
