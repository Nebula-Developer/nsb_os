using NSB.OS.Runtime.Tests;

namespace NSB.OS.Tests;

public class ScreenTests
{
    [Fact]
    public void LinkTest()
    {
        Console.WriteLine("Running print link test");
        Link.TestLink();
        Console.WriteLine("Done");
    }

    [Fact]
    public void Render() {
        RendererStack r = new RendererStack();
        Display d = new Display(new Vector2i(0, 0), new Vector2i(5, 5));
        TextElement t = new TextElement(0, 0, "Temp", new TextConfig(), new Graphics.RGB(0, 0, 0), new Graphics.RGB(0, 255, 0));
        d.AddElement(t);
        r.AddDisplay(d);
        r.Render();
        Assert.Equal('T', r.Displays[0].GetPixels().GetPixel(0, 0).Character);
    }
}