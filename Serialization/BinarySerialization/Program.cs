using System;
using System.Collections.Generic;
using BinarySerialization.Model;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace BinarySerialization;

public class Program
{
    public static void Main(string[] args)
    {
        const string fileName = "./serialized.bin";
        var department = CreateDepartment();

        // Serialize
        IFormatter formatter = new BinaryFormatter();
        Stream writeStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(writeStream, department);
        writeStream.Close();

        // Deserialize
        Stream readStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        var department2 = formatter.Deserialize(readStream) as Department;
        readStream.Close();
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

