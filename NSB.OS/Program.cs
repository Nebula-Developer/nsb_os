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
        Rectangle rect = new Rectangle(0, 0, 10, 10, new RGB(0, 0, 0), new RGB(255, 255, 255));
        d.AddElement(rect);
        d.AddElement(a);

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

            if (key == ConsoleKey.UpArrow) rect.Height--;
            if (key == ConsoleKey.DownArrow) rect.Height++;
            if (key == ConsoleKey.LeftArrow) rect.Width--;
            if (key == ConsoleKey.RightArrow) rect.Width++;
            if (key == ConsoleKey.Spacebar) rect.BG = rect.BG?.R == 0 ? new RGB(255, 255, 255) : new RGB(0, 0, 0);
            if (key == ConsoleKey.E) {
                d.Width = Console.WindowWidth;
                d.Height = Console.WindowHeight - 1;
            }

            a.Text = r.FPS.ToString();
        }
    }
}
