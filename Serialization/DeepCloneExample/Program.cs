using System;

namespace DeepCloneExample;

public class Program
{
    public static void Main(string[] args)
    {
        var person = new Person("Asad Khasanov", 21);
        var person2 = person.Clone();

        if (Object.ReferenceEquals(person, person2))
        {
            Console.WriteLine("Deep Clone did not work.");
            return;
        }

        Console.WriteLine("Deep Clone worked well.");
    }
}