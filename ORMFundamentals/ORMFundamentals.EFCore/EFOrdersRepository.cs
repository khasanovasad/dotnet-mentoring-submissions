using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Abstractions;
using ORMFundamentals.Data;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Repositories
{
    public class EFOrdersRepository : IOrderRepository
    {
        private readonly ApplicationContext context;

        public EFOrdersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Order> AddOrderAsync(string name, IEnumerable<Product> products)
        {
            var order = new Order()
            {
                Name = name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Products = products
            };

            await context.Orders!.AddAsync(order);

            await context.SaveChangesAsync();

            var result = await context.Orders.OrderBy(x => x.Id).LastAsync();

            return result;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await context.Orders!.SingleOrDefaultAsync(x => x.Id == id);

            if (order is null)
                throw new KeyNotFoundException();

            context.Orders!.Remove(order);

            await context.SaveChangesAsync();
        }

        public async Task DeleteOrdersAsync(int[] ids)
        {
            var orders = context.Orders!.Where(x => ids.Contains(x.Id));

            context.RemoveRange(orders);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await context.Orders!.SingleOrDefaultAsync(x => x.Id == id);

            if (order is null)
                throw new KeyNotFoundException();

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await context.Orders!.ToListAsync();
        }

        public async Task<Order> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await context.Orders!.SingleOrDefaultAsync(x => x.Id == id);

            if (order is null)
                throw new KeyNotFoundException();

            order.Status = status;

            await context.SaveChangesAsync();

            var result = await context.Orders!.SingleAsync(x => x.Id == id);

            return result;
        }
    }
}
