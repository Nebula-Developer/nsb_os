using System;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace Programs.LinkedProgram;

public static class Program {
    public static void Run() {
        Display display = new(new Vector2i(0, 0), new Vector2i(80, 25));
        RectangleElement rectangle = new(0, 0, 80, 25, new RGB(0, 0, 0), new RGB(0, 0, 0));
        display.AddElement(rectangle);

        RendererStack renderer = new();
        renderer.AddDisplay(display);
        renderer.Render(true);

        Console.Out.Flush();
        Console.ReadKey(true);
    }
}
