using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Console.CursorVisible = false;
        Link.TestLink();

        Display d = new Display(new Vector2i(0, 0), new Vector2i(Console.WindowWidth, Console.WindowHeight - 1));

        BarElement bar = new BarElement(0, 0, d.Width, d.Height - 1, new RGB(200, 100, 255), new RGB(255, 255, 255));
        BarElement bar2 = new BarElement(d.Width, 0, 0, d.Height - 1, new RGB(100, 200, 200), new RGB(255, 255, 255));
        
        d.AddElement(bar);
        d.AddElement(bar2);

        PointElement barPoint = new PointElement(0, 0, new RGB(155, 255, 255), new RGB(255, 255, 255));
        PointElement barPointEnd = new PointElement(0, 0, new RGB(155, 255, 255), new RGB(255, 255, 255));
        PointElement barPoint2 = new PointElement(0, d.Height - 1, new RGB(255, 155, 255), new RGB(255, 255, 255));
        PointElement barPointEnd2 = new PointElement(0, d.Height - 1, new RGB(255, 155, 255), new RGB(255, 255, 255));
        PointElement centerPoint = new PointElement(d.Width / 2, d.Height / 2, new RGB(255, 255, 255), new RGB(255, 255, 255));

        d.AddElement(barPoint);
        d.AddElement(barPointEnd);
        d.AddElement(barPoint2);
        d.AddElement(barPointEnd2);
        d.AddElement(centerPoint);

        RendererConfig conf = new RendererConfig() {
            UseThreadedRender = false,
            UseIdealRenderFrequency = true,
            RenderFrequency = 60
        };

        RendererStack r = new RendererStack();
        r.Config = conf;
        r.AddDisplay(d);
        r.StartRenderThread(true);

        while (r.IsRendering) {
            ConsoleKey? key = null;

            if (Console.KeyAvailable) {
                key = Console.ReadKey(true).Key;
            }

            if (key == ConsoleKey.Escape) {
                r.StopRenderThread();
            }

            if (key == ConsoleKey.W) { bar.start.Y--; }
            if (key == ConsoleKey.A) { bar.start.X--; }
            if (key == ConsoleKey.S) { bar.start.Y++; }
            if (key == ConsoleKey.D) { bar.start.X++; }

            if (key == ConsoleKey.UpArrow) { bar.end.Y--; }
            if (key == ConsoleKey.LeftArrow) { bar.end.X--; }
            if (key == ConsoleKey.DownArrow) { bar.end.Y++; }
            if (key == ConsoleKey.RightArrow) { bar.end.X++; }

            if (key == ConsoleKey.T) { bar2.start.Y--; }
            if (key == ConsoleKey.F) { bar2.start.X--; }
            if (key == ConsoleKey.G) { bar2.start.Y++; }
            if (key == ConsoleKey.H) { bar2.start.X++; }

            if (key == ConsoleKey.I) { bar2.end.Y--; }
            if (key == ConsoleKey.J) { bar2.end.X--; }
            if (key == ConsoleKey.K) { bar2.end.Y++; }
            if (key == ConsoleKey.L) { bar2.end.X++; }

            barPoint.X = bar.start.X;
            barPoint.Y = bar.start.Y;

            barPoint2.X = bar2.start.X - 1;
            barPoint2.Y = bar2.start.Y;

            barPointEnd.X = bar.end.X - 1;
            barPointEnd.Y = bar.end.Y;

            barPointEnd2.X = bar2.end.X;
            barPointEnd2.Y = bar2.end.Y;

            Thread.Sleep(10);
        }
    }
}
