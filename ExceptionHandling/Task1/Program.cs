using System;
using System.Linq;

namespace Task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = String.Empty;
            while (true)
            {
                Console.Write("Enter (.quit to exit): ");
                input = Console.ReadLine();

                try
                {
                    char firstChar = input.First();

                    if (input.Equals(".quit"))
                    {
                        break;
                    }

                    Console.WriteLine($"The first character of the input is: {firstChar}");
                }
                catch (InvalidOperationException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input is empty.\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}