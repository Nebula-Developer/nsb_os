
namespace NSB.OS.Tests;

public class ScreenTests
{
    [Fact]
    public void Screen()
    {
        Console.WriteLine("Running print test");
        Display.TestLink();
        Console.WriteLine("Done");
    }
}