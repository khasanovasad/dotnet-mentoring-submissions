using NUnit.Framework;
using Task2.Presentation;

namespace TestTask1;

[TestFixture]
public class Tests
{
    [Test]
    public void ConsolePresenter_ShouldOnlyExistOncePerProgram_BecauseItIsSingleton()
    {
        IPresenter presenter1 = ConsolePresenter.Instance;
        IPresenter presenter2 = ConsolePresenter.Instance;
        
        Assert.AreSame(presenter1, presenter2);
    }
}