using System;
using System.Collections.Generic;

namespace JsonSerialization.Model;

public class Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }

    public Department(string departmentName, List<Employee> employees)
    {
        DepartmentName = departmentName;
        Employees = employees;
    }
}

