using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Data;
using ORMFundamentals.Models;
using ORMFundamentals.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ORMFundamentals.EFCore.Tests
{
    public class EFOrdersRepositoryTests
    {
        private readonly EFOrdersRepository repository;
        private readonly ApplicationContext context;

        public EFOrdersRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDataBase")
                .Options;

            context = new ApplicationContext(options);
            repository = new(context);
        }

        [Fact]
        public async Task AddOrder_For2NewOgrders_ShouldIncreaseCountOfOrdersBy2()
        {
            var products = new List<Product>() { new Product { Name = "Product1" }, };

            await repository.AddOrderAsync("Order1", products);
            await repository.AddOrderAsync("Order2", products);
            var result = (await repository.GetOrdersAsync()).Count();

            result.Should().Be(2);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task AddOrder_ForNewOrder_ShouldAddNewOrder()
        {
            var products = new List<Product>() { new Product { Name = "Product1" }, };

            var order = await repository.AddOrderAsync("Order1", products);

            var result = await repository.GetOrderAsync(order.Id);

            result.Name.Should().Be(order.Name);
            await context.Database.EnsureDeletedAsync();
        }


        [Fact]
        public async Task DeleteOrder_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            Func<Task> func = async () => await repository.DeleteOrderAsync(1);

            await func.Should().ThrowAsync<KeyNotFoundException>();
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task DeleteOrder_ForOnlyExistingId_ShouldMakeCountOfNumbersEqual0()
        {
            var products = new List<Product>() { new Product { Name = "Product1" } };

            var order = await repository.AddOrderAsync("Order1", products);

            await repository.DeleteOrderAsync(order.Id);

            var result = (await repository.GetOrdersAsync()).Count();

            result.Should().Be(0);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetOrder_ForExistingId_ShouldReturnOrder()
        {
            var products = new List<Product>() { new Product { Name = "Product1" }, };

            var order = await repository.AddOrderAsync("Order1", products);

            var result = await repository.GetOrderAsync(order.Id);

            result.Id.Should().Be(order.Id);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetOrder_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            Func<Task> func = async () => await repository.GetOrderAsync(0);

            await func.Should().ThrowAsync<KeyNotFoundException>();
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetOrders_For2Orders_ShouldReturn2Orders()
        {
            var products = new List<Product>() { new Product { Name = "Product1" }, };

            await repository.AddOrderAsync("Order1", products);
            await repository.AddOrderAsync("Order2", products);
            var result = (await repository.GetOrdersAsync()).Count();

            result.Should().Be(2);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UpdateOrderStatus_ForExistingId_ShouldUpdateStatus()
        {
            var products = new List<Product>() { new Product { Name = "Products" } };

            var order = await repository.AddOrderAsync("Order1", products);

            var result = await repository.UpdateOrderStatusAsync(order.Id, "Done");

            result.Status.Should().Be("Done");
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UpdateOrderStatus_ForNonExistingId_ShouldThrowKeyNotFoundException()
        {
            var func = async () => await repository.UpdateOrderStatusAsync(0, "");

            await func.Should().ThrowAsync<KeyNotFoundException>();
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task DeleteOrders_ForExistingIds_ShouldRemoveSeveralOrders()
        {
            var products = new List<Product>() { new Product { Name = "Product1" }, };

            var order1 = await repository.AddOrderAsync("Order1", products);
            var order2 = await repository.AddOrderAsync("Order2", products);
            await repository.DeleteOrdersAsync(new int[2] { order1.Id, order2.Id });
            var result = (await repository.GetOrdersAsync()).Count();

            result.Should().Be(0);
            await context.Database.EnsureDeletedAsync();
        }
    }
}