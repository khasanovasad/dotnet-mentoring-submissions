using System;

namespace JsonSerialization.Model;

public class Employee
{
    public string EmployeeName { get; set; }

    public Employee(string employeeName)
    {
        EmployeeName = employeeName;
    }
}

