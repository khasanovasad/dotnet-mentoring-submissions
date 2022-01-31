using System;
using MileFields;
using NUnit.Framework;

namespace MineFields.Tests;

public class MineFieldsSolverTests
{
    private MineFieldsSolver? _mineFieldsSolver;
    
    [SetUp]
    public void Setup()
    {
        _mineFieldsSolver = new MineFieldsSolver();
    }

    [Test]
    public void Solve_Should_When()
    {
        Assert.Pass();
    }

    [Test]
    public void AdjacencyList_ShouldThrowArgumentException_WhenProvidedNegativeInput()
    {
        const int vIndex = -1;
        const int hIndex = -1;

        const int hCount = -3;
        const int vCount = -4;

        Assert.Throws<ArgumentOutOfRangeException>(
            () => { _mineFieldsSolver!.AdjacencyList(vIndex, hIndex, vCount, hCount); });
    }

    [Test]
    public void AdjacencyList_ShouldReturnCorrectAdjacencyList_WhenProvidedWithValidInput()
    {
        /*
                x, x, x, 
                x, (), x, 
                x, x, x, 
                _, _, _, 
         
         */
        IndexIn2D[] expectedResult = new[]
        {
            new IndexIn2D() {VIndex = 0, HIndex = 0},
            new IndexIn2D() {VIndex = 0, HIndex = 1},
            new IndexIn2D() {VIndex = 0, HIndex = 2},
            new IndexIn2D() {VIndex = 1, HIndex = 0},
            new IndexIn2D() {VIndex = 1, HIndex = 2},
            new IndexIn2D() {VIndex = 2, HIndex = 0},
            new IndexIn2D() {VIndex = 2, HIndex = 1},
            new IndexIn2D() {VIndex = 2, HIndex = 2},
        };
        
        const int vIndex = 1;
        const int hIndex = 1;

        const int hCount = 3;
        const int vCount = 4;

        var actualResult = _mineFieldsSolver!.AdjacencyList(vIndex, hIndex, vCount, hCount);
        
        Assert.AreEqual(expectedResult, actualResult);
    }
}