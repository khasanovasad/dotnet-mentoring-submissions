using System;
using ClassLibrary;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            Console.WriteLine(OutputFormatter.FormatOutput(name));
        }
    }
}
