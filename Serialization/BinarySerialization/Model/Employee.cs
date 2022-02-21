using System;

namespace BinarySerialization.Model;

[Serializable]
public class Employee
{
    public string EmployeeName { get; set; }

    public Employee() : this(String.Empty) { }

    public Employee(string employeeName)
    {
        EmployeeName = employeeName;
    }
}

