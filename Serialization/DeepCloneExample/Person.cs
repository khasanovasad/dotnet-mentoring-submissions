using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCloneExample;

[Serializable]
public class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person() : this(String.Empty, 0) { }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public object Clone()
    {
        using var memStream = new MemoryStream();
        if (this.GetType().IsSerializable)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memStream, this);
            memStream.Position = 0;
            return formatter.Deserialize(memStream);
        }

        return null;
    }
}
