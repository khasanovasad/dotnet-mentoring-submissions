using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using JsonSerialization.Model;
using System.IO;
using System.Threading.Tasks;

namespace JsonSerialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        var department = CreateDepartment();
        var jsonString = JsonSerializer.Serialize(department, options: new JsonSerializerOptions { WriteIndented = true });

        const string fileName = "./serialized.json";
        await File.WriteAllTextAsync(fileName, jsonString);

        var jsonString2 = File.ReadAllText(fileName);
        var department2 = JsonSerializer.Deserialize<Department>(jsonString2);
    }

    private static Department CreateDepartment()
    {
        string departmentName = "Information Security";
        var department = new Department(departmentName, new List<Employee>());
        for (int i = 1; i <= 10; ++i)
        {
            var employee = new Employee($"EmployeeName{i}");
            department.Employees.Add(employee);
        }

        return department;
    }
}

