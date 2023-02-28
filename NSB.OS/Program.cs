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
        TextElement a = new TextElement(0, 0, "Hello World!");
        //Rectangle rect = new Rectangle(0, 0, 10, 10, new RGB(0, 0, 0), new RGB(255, 255, 255));
        d.AddElement(a);
        //d.AddElement(rect);

        RendererStack r = new RendererStack();
        r.AddDisplay(d);
        r.StartRenderThread(true);

        while (r.IsRendering) {
            ConsoleKey key = Console.ReadKey(true).Key;
            a.Text = key.ToString();

            if (key == ConsoleKey.Escape) {
                r.StopRenderThread();
            }

            // if (key == ConsoleKey.UpArrow) rect.Height--;
            // if (key == ConsoleKey.DownArrow) rect.Height++;
            // if (key == ConsoleKey.LeftArrow) rect.Width--;
            // if (key == ConsoleKey.RightArrow) rect.Width++;
            // if (key == ConsoleKey.Spacebar) rect.BG = new RGB(255, 255, 255);
        }
    }
}
