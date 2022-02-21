using System;
using System.Collections.Generic;

namespace XmlSerialization.Model;

public class Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }

    public Department() : this(String.Empty, new List<Employee>()) { }

    public Department(string departmentName, List<Employee> employees)
    {
        DepartmentName = departmentName;
        Employees = employees;
    }
}

