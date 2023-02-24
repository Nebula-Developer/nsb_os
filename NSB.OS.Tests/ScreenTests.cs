using NSB.OS.Runtime.Tests;

namespace NSB.OS.Tests;

public class ScreenTests
{
    [Fact]
    public void Screen()
    {
        Console.WriteLine("Running print link test");
        Link.TestLink();
        Console.WriteLine("Done");
    }
}