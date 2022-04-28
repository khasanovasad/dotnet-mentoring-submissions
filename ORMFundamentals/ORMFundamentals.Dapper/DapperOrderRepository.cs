using Dapper;
using ORMFundamentals.Abstractions;
using ORMFundamentals.Models;
using System.Data;
using System.Linq.Expressions;

namespace ORMFundamentals.Dapper
{
    public class DapperOrderRepository : IOrderRepository
    {
        private readonly IDbConnection dbConnection;

        public DapperOrderRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public Task<Order> AddOrderAsync(string name, IEnumerable<Product> products)
        {
            return dbConnection.QuerySingleAsync<Order>($@"INSERT INTO Orders OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Status, INSERTED.CreatedDate, INSERTED.UpdatedDate VALUES('{name}','NotStarted','{DateTime.Now}','{DateTime.Now}') ");
        }

        public Task DeleteOrderAsync(int id)
        {
            return dbConnection.ExecuteAsync($"DELETE FROM Orders WHERE Id={id}");
        }

        public Task<Order> GetOrderAsync(int id)
        {
            return dbConnection.QuerySingleAsync<Order>($"SELECT * FROM Orders WHERE Id={id}");
        }

        public async Task DeleteOrdersAsync(int[] ids)
        {
            var tasks = new List<Task>();
            foreach (var id in ids)
                await dbConnection.ExecuteAsync($"DELETE FROM Orders WHERE Id={id}");
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return dbConnection.QueryAsync<Order>("GetOrders", commandType: CommandType.StoredProcedure);
        }

        public async Task<Order> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await dbConnection.QuerySingleAsync<Order>(@$"UPDATE Orders 
SET Status = '{status}' 
OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Status, INSERTED.CreatedDate, INSERTED.UpdatedDate
WHERE Id={id}");

            order.Products = await dbConnection.QueryAsync<Product>($"SELECT * FROM Products WHERE OrderId={id}");

            return order;
        }
    }
}