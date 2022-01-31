using System;

namespace FizzBuzz;

public class FizzBuzzer
{
    public string AcceptNumber(int number)
    {
        if (number % 3 == 0 && number % 5 == 0)
        {
            return "FizzBuzz";
        }
        
        if (number % 3 == 0)
        {
            return "Fizz";
        }
        
        if (number % 5 == 0)
        {
            return "Buzz";
        }
        
        return number.ToString();
    }

    public void FizzBuzzPrint()
    {
        string[] fizzBuzz = FizzBuzz();

        Array.ForEach(fizzBuzz, item => Console.WriteLine(item));
    }
    
    public string[] FizzBuzz()
    {
        var result = new string[100];
        for (int i = 0; i < 100; ++i)
        {
            result[i] = AcceptNumber(i + 1);
        }

        return result;
    }
}
