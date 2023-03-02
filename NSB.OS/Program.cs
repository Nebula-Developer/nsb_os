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

        BezeirBarElement b = new BezeirBarElement(0, 0, 10, 10, new Vector2i[] {
            new Vector2i(0, 0),
            new Vector2i(0, 0)
        }, new RGB(255, 100, 255), null);
        d.AddElement(b);

        PointRange range = new PointRange(0, 0, null);
        d.AddElement(range);

        RendererConfig conf = new RendererConfig() {
            RenderFrequency = 60
        };

        RendererStack r = new RendererStack();
        r.Config = conf;
        r.AddDisplay(d);
        // r.StartRenderThread(true);
        int selectedBezeirPoint = 0;
        
        while (true) {
            r.Render();
            ConsoleKey? key = null;
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape) {
                r.StopRenderThread();
            }
            
            if (key == ConsoleKey.UpArrow) { b.start.Y--; }
            if (key == ConsoleKey.DownArrow) { b.start.Y++; }
            if (key == ConsoleKey.LeftArrow) { b.start.X--; }
            if (key == ConsoleKey.RightArrow) { b.start.X++; }

            if (key == ConsoleKey.O) {
                selectedBezeirPoint++;
                if (selectedBezeirPoint >= b.points.Length) selectedBezeirPoint = 0;
            }

            if (key == ConsoleKey.L) {
                selectedBezeirPoint--;
                if (selectedBezeirPoint < 0) selectedBezeirPoint = b.points.Length - 1;
            }

            if (key == ConsoleKey.W) { b.points[selectedBezeirPoint].Y--; }
            if (key == ConsoleKey.S) { b.points[selectedBezeirPoint].Y++; }
            if (key == ConsoleKey.A) { b.points[selectedBezeirPoint].X--; }
            if (key == ConsoleKey.D) { b.points[selectedBezeirPoint].X++; }

            List<PointElement> p = new List<PointElement>(b.points.Length);
            for (int i = 0; i < b.points.Length; i++) {
                p.Add(new PointElement(b.points[i].X, b.points[i].Y, selectedBezeirPoint == i ? new RGB(255, 255, 255) : new RGB(255, 0, 0)));
            }

            range.Points = p;
        }
    }
}
