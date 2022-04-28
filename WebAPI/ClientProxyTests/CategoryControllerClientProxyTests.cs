using FluentAssertions;
using ProxyClient.TestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;

namespace ClientProxyTests
{
    public class CategoryControllerClientProxyTests
    {
        private readonly Client client;
        private readonly NorthwindContext context;

        public CategoryControllerClientProxyTests()
        {
            context = new NorthwindContext();
            client = new Client("https://localhost:7278/");
        }

        [Fact]
        public async Task CategoryController_GetCategories_ShouldReturnAllCategories()
        {
            var result = (await client.ApiCategoriesGetAsync()).Count;

            result.Should().Be(context.Categories.Count());
        }

        [Fact]
        public async Task CategoryController_AddCategory_ShouldIncreaseCountOfCategoriesBy1()
        {
            var category = await client.ApiCategoriesPostAsync(new() { CategoryName = "Category" });

            var result = (await client.ApiCategoriesGetAsync(category.CategoryId)).CategoryName;

            result.Should().Be(category.CategoryName);
        }

        [Fact]
        public async Task CategoryController_DeleteCategory_ShouldDeacreseCountOfCategoryBy1()
        {
            var expectedValue = context.Categories.Count();
            var category = (await client.ApiCategoriesPostAsync(new() { CategoryName = "Category" }));

            await client.ApiCategoriesDeleteAsync(category.CategoryId);

            var result = (await client.ApiCategoriesGetAsync()).Count();

            result.Should().Be(expectedValue);
        }

        [Fact]
        public async Task CategoriesController_UpdateCategory_ShouldUpdateCategory()
        {
            var expectedValue = "CategoryName";
            var category = await client.ApiCategoriesPostAsync(new() { CategoryName = "Category" });

            await client.ApiCategoriesPutAsync(category.CategoryId, new() { CategoryId = category.CategoryId, CategoryName = expectedValue });

            var result = (await client.ApiCategoriesGetAsync(category.CategoryId)).CategoryName;

            result.Should().Be(expectedValue);

            await client.ApiCategoriesDeleteAsync(category.CategoryId);
        }
    }
}
