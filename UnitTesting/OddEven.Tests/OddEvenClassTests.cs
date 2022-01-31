using System;
using NUnit.Framework;

namespace OddEven.Tests;

public class OddEvenClassTests
{
    private OddEvenClass? _oddEvenClass;
    
    [SetUp]
    public void Setup()
    {
        _oddEvenClass = new OddEvenClass();
    }

    [Test]
    public void AcceptNumber_ShouldThrowArgumentOutOfRangeException_WhenNumberMoreThan100IsPassed()
    {
        const int invalidNumber = 101;
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _oddEvenClass!.AcceptNumber(invalidNumber);
        });
    }
    
    [Test]
    public void AcceptNumber_ShouldThrowArgumentOutOfRangeException_WhenNumberLessThan0IsPassed()
    {
        const int invalidNumber = -1;
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _oddEvenClass!.AcceptNumber(invalidNumber);
        });
    }

    [Test]
    public void AcceptNumber_ShouldReturnOdd_WhenNumberPassedIsOdd()
    {
        const int oddNumber = 69;
        const string expectedResult = "Odd";

        string actualResult = _oddEvenClass!.AcceptNumber(oddNumber);
        
        Assert.AreEqual(expectedResult, actualResult);
    }
    
    [Test]
    public void AcceptNumber_ShouldReturnEven_WhenNumberPassedIsEven()
    {
        const int evenNumber = 68;
        const string expectedResult = "Even";

        string actualResult = _oddEvenClass!.AcceptNumber(evenNumber);
        
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(13)]
    [TestCase(17)]
    public void AcceptNumber_ShouldReturnTheNumber_WhenNumberIsPrime(int primeNumber)
    {
        string expectedResult = primeNumber.ToString();

        string actualResult = _oddEvenClass!.AcceptNumber(primeNumber);
        
        Assert.AreEqual(expectedResult, actualResult);
    }
}