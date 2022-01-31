namespace OddEven;

public class OddEvenClass
{
    public const int Min = 0;
    public const int Max = 100;

    public void PrintTer()
    {
        string[] result = Worker();
        Array.ForEach(result, item =>
        {
            Console.WriteLine();
        });
    }

    public string[] Worker()
    {
        var list = new List<string>();

        for (int i = Min; i <= 100; ++i)
        {
            string resultingString = AcceptNumber(i);
            list.Add(resultingString);
        }

        return list.ToArray();
    }
    
    public string AcceptNumber(int number)
    {
        if (number is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (IsPrime(number))
        {
            return number.ToString();
        }
        
        if (number % 2 == 0)
        {
            return "Even";
        }

        return "Odd";
    }
    
    private bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));
          
        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0)
            {
                return false;
            }
        }
    
        return true;        
    } 
}