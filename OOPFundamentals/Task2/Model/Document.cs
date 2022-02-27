using System;
using System.Reflection;
using System.Text;

namespace Task2.Model;

public abstract class Document
{
    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        foreach (PropertyInfo propInfo in this.GetType().GetProperties())
        {
            strBuilder.Append(propInfo.Name);
            strBuilder.Append(": ");

            if (propInfo.PropertyType.IsArray)
            {
                var value = propInfo.GetValue(this, null) as string[];
                strBuilder.Append(String.Join(", ", value!));
                strBuilder.Append('\n');
                continue;
            }

            strBuilder.Append(propInfo.GetValue(this, null)!.ToString());
            strBuilder.Append('\n');
        }

        return strBuilder.ToString();
    }
}
