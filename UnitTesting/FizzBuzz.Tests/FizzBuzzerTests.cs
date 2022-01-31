using NUnit.Framework;

namespace FizzBuzz.Tests;

[TestFixture]
public class FizzBuzzerTests
{
    private FizzBuzzer? _fizzBuzzer;

    [SetUp]
    public void SetUp()
    {
        _fizzBuzzer = new FizzBuzzer();
    }
    
    [TestCase(3)]
    [TestCase(6)]
    [TestCase(9)]
    [TestCase(18)]
    [TestCase(36)]
    public void AcceptNumber_ShouldReturnFizz_WhenNumberIsDivisibleBy3(int number)
    {
        const string expectedResult = "Fizz";
        string result = _fizzBuzzer!.AcceptNumber(number);
        
        Assert.AreEqual(expectedResult, result);
    }
    
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(25)]
    [TestCase(35)]
    public void AcceptNumber_ShouldReturnBuzz_WhenNumberIsDivisibleBy5(int number)
    {
        const string expectedResult = "Buzz";
        string result = _fizzBuzzer!.AcceptNumber(number);
        
        Assert.AreEqual(expectedResult, result);
    }
    
    [TestCase(15)]
    [TestCase(30)]
    [TestCase(45)]
    [TestCase(60)]
    [TestCase(75)]
    public void AcceptNumber_ShouldReturnFizzBuzz_WhenNumberIsDivisibleBy3And5(int number)
    {
        const string expectedResult = "FizzBuzz";
        string result = _fizzBuzzer!.AcceptNumber(number);
        
        Assert.AreEqual(expectedResult, result);
    }

    [TestCase(1)]
    [TestCase(16)]
    [TestCase(34)]
    [TestCase(76)]
    [TestCase(91)]
    public void AcceptNumber_ShouldReturnNumber_WhenNumberIsNotDivisibleBy3And5(int number)
    {
        string expectedResult = number.ToString();
        string result = _fizzBuzzer!.AcceptNumber(number);
        
        Assert.AreEqual(expectedResult, result);
    }
}