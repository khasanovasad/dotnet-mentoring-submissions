using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomSerializable;

public class Program
{
    public static void Main(string[] args)
    {
        const string fileName = "./serialized.person";
        var person = new Person("Asad Khasanov", 21);

        IFormatter formatter = new BinaryFormatter();

        // Serialize
        var writeStream = new FileStream(fileName, FileMode.Create);
        formatter.Serialize(writeStream, person);
        writeStream.Close();

        // Deserialize
        var readStream = new FileStream(fileName, FileMode.Open);
        Person person2 = (Person)formatter.Deserialize(readStream);

        Console.WriteLine(person);
        Console.WriteLine(person2);
    }
}