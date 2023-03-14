using System;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace Programs.LinkedProgram;

public static class Program {
    public static void Run() {
        Display display = new Display(new Vector2i(0, 0), new Vector2i(80, 25));
        RectangleElement rectangle = new RectangleElement(0, 0, 80, 25, new RGB(0, 0, 0), new RGB(0, 0, 0));
        display.AddElement(rectangle);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(display);
        renderer.Render(true);

        Console.Out.Flush();
        Console.ReadKey(true);
    }
}
