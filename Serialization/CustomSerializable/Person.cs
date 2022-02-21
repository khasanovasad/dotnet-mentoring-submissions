using System;
using System.Runtime.Serialization;

namespace CustomSerializable;

[Serializable]
public class Person : ISerializable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person() : this(String.Empty, 0) { }

    public Person(string name, int age) 
    { 
        Name = name;
        Age = age;
    }

    public Person(SerializationInfo info, StreamingContext context)
    {
        Age = (int) info.GetValue("Age", typeof(int));
        Name = (string) info.GetValue("Name", typeof(string));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Age", Age, typeof(int));
        info.AddValue("Name", Name, typeof(string));
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}

