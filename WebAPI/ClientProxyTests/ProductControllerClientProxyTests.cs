using FluentAssertions;
using ProxyClient.TestClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;

namespace ClientProxyTests
{
    public class ProductControllerClientProxyTests
    {
        private readonly Client client;
        private readonly NorthwindContext context;

        public ProductControllerClientProxyTests()
        {
            context = new NorthwindContext();
            client = new Client("https://localhost:7278/");
        }

        [Fact]
        public async Task ProductController_GetProducts_ShouldReturnAllProducts()
        {
            var result = (await client.ApiProductsGetAsync()).Count;

            result.Should().Be(context.Products.Count());
        }

        [Fact]
        public async Task ProductController_AddProduct_ShouldIncreaseCountOfProductsBy1()
        {
            var product = await client.ApiProductsPostAsync(new() { ProductName = "Product1", CategoryId = 1 });

            var result = (await client.ApiProductsGetAsync(product.ProductId)).ProductName;

            result.Should().Be(product.ProductName);

            await client.ApiProductsDeleteAsync(product.ProductId);
        }

        [Fact]
        public async Task ProductController_DeleteProduct_ShouldDeacreseCountOfProductBy1()
        {
            var expectedValue = context.Products.Count();
            var product = (await client.ApiProductsPostAsync(new() { ProductName = "Product1" }));

            await client.ApiProductsDeleteAsync(product.ProductId);

            var result = (await client.ApiProductsGetAsync()).Count();

            result.Should().Be(expectedValue);
        }

        [Fact]
        public async Task ProductController_UpdateProduct_ShouldUpdateProduct()
        {
            var expectedValue = "ProductName";
            var product = await client.ApiProductsPostAsync(new() { ProductName = "Produc1" });

            await client.ApiProductsPutAsync(product.ProductId, new() { ProductId = product.ProductId, ProductName = expectedValue });

            var result = (await client.ApiProductsGetAsync(product.ProductId)).ProductName;

            result.Should().Be(expectedValue);

            await client.ApiProductsDeleteAsync(product.ProductId);
        }
    }
}