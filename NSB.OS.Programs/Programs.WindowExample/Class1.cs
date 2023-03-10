using System;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace Programs.WindowExample;

public static class Program
{
    public static void Run()
    {
        Display display = new Display(new Vector2i(0, 0), new Vector2i(80, 25));
        BezeirBarElement bar = new BezeirBarElement(0, 0, Console.WindowWidth - 1, Console.WindowHeight - 1, new Vector2i[] {
            new Vector2i(0, 0)
        }, new RGB(0, 0, 0), new RGB(255, 255, 255));
        display.AddElement(bar);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(display);
        renderer.Render(true);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.W) bar.points[0].Y--;
            if (key.Key == ConsoleKey.A) bar.points[0].X--;
            if (key.Key == ConsoleKey.S) bar.points[0].Y++;
            if (key.Key == ConsoleKey.D) bar.points[0].X++;

            renderer.Render();
        }
    }
}
 