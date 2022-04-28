using ORMFundamentals.Models;
using System.Linq.Expressions;

namespace ORMFundamentals.Abstractions
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();

        public Task<Order> GetOrderAsync(int id);

        public Task DeleteOrderAsync(int id);

        public Task DeleteOrdersAsync(int[] ids);

        public Task<Order> AddOrderAsync(string name, IEnumerable<Product> products);

        public Task<Order> UpdateOrderStatusAsync(int id, string status);
    }
}