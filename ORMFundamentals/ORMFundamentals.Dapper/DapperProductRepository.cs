using Dapper;
using ORMFundamentals.Abstractions;
using ORMFundamentals.DTO;
using ORMFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFundamentals.Dapper
{
    public class DapperProductRepository : IProductRepositroy
    {
        private readonly IDbConnection dbConnection;

        public DapperProductRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public Task<Product> AddProductAsync(AddProductDTO dto)
        {
            return dbConnection.QuerySingleAsync<Product>($@"INSERT INTO Products (Name, Description, Weight, Height, Width, Length)
OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Description, INSERTED.Weight, INSERTED.Height, INSERTED.Width, INSERTED.Length
VALUES('{dto.Name}','{dto.Description}',{dto.Weight},{dto.Height},{dto.Width},{dto.Length})");
        }

        public Task DeleteProductAsync(int id)
        {
            return dbConnection.ExecuteAsync($"DELETE FROM Products WHERE Id={id}");
        }

        public Task<Product> GetProductAsync(int id)
        {
            return dbConnection.QuerySingleAsync<Product>($"SELECT * FROM Products WHERE Id={id}");
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return dbConnection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public Task<Product> UpdateProductAsync(int id, UpdateProductDTO dto)
        {
            return dbConnection.QuerySingleAsync<Product>(@$"UPDATE Products 
SET Name = '{dto.Name}', Description = '{dto.Description}', Weight = {dto.Weight}, Height = {dto.Height}, Width = {dto.Width}, Length = {dto.Length}
OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Description, INSERTED.Weight, INSERTED.Height, INSERTED.Width, INSERTED.Length
WHERE Id={id}");
        }
    }
}
