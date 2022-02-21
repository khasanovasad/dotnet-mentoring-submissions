using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using XmlSerialization.Model;
using System.IO;

namespace XmlSerialization;

public class Program
{
    public static void Main(string[] args)
    {
        const string fileName = "./serialized.xml";
        var department = CreateDepartment();

        var serializer = new XmlSerializer(typeof(Department));
        var writer = XmlWriter.Create(fileName, new XmlWriterSettings { Indent = true });
        serializer.Serialize(writer, department);
        writer.Close();

        using Stream reader = new FileStream(fileName, FileMode.Open);
        var department2 = serializer.Deserialize(reader) as Department;
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

