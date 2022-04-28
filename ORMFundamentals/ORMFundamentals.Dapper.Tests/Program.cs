using Microsoft.Data.SqlClient;
using ORMFundamentals.Dapper;
using ORMFundamentals.Models;
using System.Text.Json;

public class Program
{
    static string connectionString = @"Server=.\SQLEXPRESS;Database=ORMFundamentals;Trusted_Connection=True;";
    static SqlConnection connection = new(connectionString);
    static List<Product> products = new() { new Product { Name = "Product1" }, };
    static JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };
    static DapperOrderRepository orderRepository = new(connection);
    static DapperProductRepository productRepository = new(connection);

    public static async Task Main()
    {
        Console.WriteLine("Enter 1 to check Order repository and 2 to check Product repository");

        var input = Console.ReadLine();

        Console.WriteLine("Press number to test appropriate functionality");

        if (input == "1")
            await ShowOrderProgram();
        else
            await ShowProductProgram();

        Console.WriteLine("Press Y(es) in order to check another repository");

        if (Console.ReadLine() == "Y")
            await Main();
        else
            Console.WriteLine("Good bye");
    }

    #region Order program
    static async Task ShowOrderProgram()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Now you testing Order dapper repository");
        Console.ResetColor();
        Console.WriteLine("1: Add order");
        Console.WriteLine("2: Get orders");
        Console.WriteLine("3: Get order");
        Console.WriteLine("4: Delete order");
        Console.WriteLine("5: Update order status");
        Console.WriteLine("6: Delete orders in bulk");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                await AddOrderAsync();
                break;
            case "2":
                await ShowOrdersAsync();
                break;
            case "3":
                await ShowOrderAsync();
                break;
            case "4":
                await DeleteOrderAsync();
                break;
            case "5":
                await UpdateOrderStatusAsync();
                break;
            case "6":
                await DeleteOrdersInBulkAsync();
                break;
        }

        Console.WriteLine("Enter Y(es) to repeat");
        input = Console.ReadLine();
        if (input == "Y" || input == "y")
            await ShowOrderProgram();
    }

    static async Task AddOrderAsync()
    {
        Console.WriteLine("Enter order name:");
        var name = Console.ReadLine();
        var order = await orderRepository.AddOrderAsync(name!, products);

        Console.WriteLine("Created order:");

        var jsonString = JsonSerializer.Serialize(order, jsonSerializerOptions);
        Console.WriteLine(jsonString);
    }

    static async Task ShowOrdersAsync()
    {
        var orders = await orderRepository.GetOrdersAsync();
        Console.WriteLine("List of orders:");
        foreach (var order in orders)
        {
            string jsonString = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(jsonString);
        }
    }

    static async Task ShowOrderAsync()
    {
        Console.WriteLine("Enter order id");
        var id = Console.ReadLine();
        var order = await orderRepository.GetOrderAsync(Convert.ToInt32(id));
        string jsonString = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(jsonString);
    }

    static async Task DeleteOrderAsync()
    {
        Console.WriteLine("Enter order id:");
        var id = Console.ReadLine();
        await orderRepository.DeleteOrderAsync(Convert.ToInt32(id));
    }

    static async Task UpdateOrderStatusAsync()
    {
        Console.WriteLine("Enter order id:");
        var id = Console.ReadLine();

        Console.WriteLine("Enter new status:");
        var status = Console.ReadLine();

        var order = await orderRepository.UpdateOrderStatusAsync(Convert.ToInt32(id), status);

        var jsonString = JsonSerializer.Serialize(order, jsonSerializerOptions);
        Console.WriteLine(jsonString);
    }

    static async Task DeleteOrdersInBulkAsync()
    {
        Console.WriteLine("Enter ids of orders to delete in this pattern:1,2,3,4....");
        var input = Console.ReadLine();
        var ids = input.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        await orderRepository.DeleteOrdersAsync(ids);
    }
    #endregion

    #region Product program
    static async Task ShowProductProgram()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Now you test Product dapper repository");
        Console.ResetColor();

        Console.WriteLine("1: Add product");
        Console.WriteLine("2: Get product");
        Console.WriteLine("3: Get products");
        Console.WriteLine("4: Delete product");
        Console.WriteLine("5: Update product");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                await AddProductAsync();
                break;
            case "2":
                await ShowProductAsync();
                break;
            case "3":
                await ShowProductsAsync();
                break;
            case "4":
                await DeleteProductAsync();
                break;
            case "5":
                await UpdateProductAsync();
                break;
        }

        Console.WriteLine("Enter Y(es) to repeat");
        input = Console.ReadLine();
        if (input == "Y" || input == "y")
            await ShowProductProgram();
    }

    static async Task AddProductAsync()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();
        Console.Write("Enter description: ");
        var description = Console.ReadLine();
        Console.Write("Enter weight: ");
        var weight = Console.ReadLine();
        Console.Write("Enter height: ");
        var height = Console.ReadLine();
        Console.Write("Enter width: ");
        var width = Console.ReadLine();
        Console.Write("Enter length: ");
        var length = Console.ReadLine();

        var product = await productRepository.AddProductAsync(new()
        {
            Name = name,
            Description = description,
            Height = Convert.ToInt32(height),
            Length = Convert.ToInt32(length),
            Weight = Convert.ToInt32(weight),
            Width = Convert.ToInt32(width)
        });

        Console.WriteLine("Create product");

        var jsonString = JsonSerializer.Serialize(product, jsonSerializerOptions);
        Console.WriteLine(jsonString);
    }

    static async Task ShowProductsAsync()
    {
        var products = await productRepository.GetProductsAsync();
        Console.WriteLine("List of products:");
        foreach (var product in products)
        {
            string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(jsonString);
        }
    }

    static async Task ShowProductAsync()
    {
        Console.WriteLine("Enter product id");
        var id = Console.ReadLine();
        var product = await productRepository.GetProductAsync(Convert.ToInt32(id));
        string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(jsonString);
    }

    static async Task DeleteProductAsync()
    {
        Console.WriteLine("Enter order id:");
        var id = Console.ReadLine();
        await productRepository.DeleteProductAsync(Convert.ToInt32(id));
    }

    static async Task UpdateProductAsync()
    {
        Console.Write("Enter id: ");
        var id = Console.ReadLine();
        Console.Write("Enter name: ");
        var name = Console.ReadLine();
        Console.Write("Enter description: ");
        var description = Console.ReadLine();
        Console.Write("Enter weight: ");
        var weight = Console.ReadLine();
        Console.Write("Enter height: ");
        var height = Console.ReadLine();
        Console.Write("Enter width: ");
        var width = Console.ReadLine();
        Console.Write("Enter length: ");
        var length = Console.ReadLine();

        var product = await productRepository.UpdateProductAsync(Convert.ToInt32(id), new()
        {
            Name = name,
            Description = description,
            Width = Convert.ToInt32(width),
            Weight = Convert.ToInt32(weight),
            Height = Convert.ToInt32(height),
            Length = Convert.ToInt32(length)
        });

        Console.WriteLine("Updated product");

        var jsonString = JsonSerializer.Serialize(product, jsonSerializerOptions);
        Console.WriteLine(jsonString);
    }
    #endregion
}