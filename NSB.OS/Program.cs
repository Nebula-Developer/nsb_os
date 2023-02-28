using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics.PanelNS;
using NSB.OS.Runtime.Tests;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Link.TestLink();

        Panel p = new Panel(new Vector2i(0, 0), new Vector2i(10, 10), new Display(new Vector2i(0, 0), new Vector2i(Console.WindowWidth, Console.WindowHeight - 1)));
        TextElement a = new TextElement(0, 0, "Hello World!");
        Rectangle rect = new Rectangle(0, 0, 10, 10, new RGB(0, 0, 0), new RGB(255, 255, 255));
        p.AddElement(a);
        p.AddElement(rect);
        p.UpdateDisplay();

        Renderer r = new Renderer();
        r.Displays.Add(p.display);
        r.StartRenderThread(true);

        while (r.IsRendering) {
            ConsoleKey key = Console.ReadKey(true).Key;
            a.Text = key.ToString();

            if (key == ConsoleKey.Escape) {
                r.StopRenderThread();
            }

            if (key == ConsoleKey.UpArrow) rect.Height++;
            if (key == ConsoleKey.DownArrow) rect.Height--;
            if (key == ConsoleKey.LeftArrow) rect.Width--;
            if (key == ConsoleKey.RightArrow) rect.Width++;
            rect.BG = new RGB((byte) (rect.BG.R + 1), (byte) (rect.BG.G + 1), (byte) (rect.BG.B + 1));

            p.UpdateDisplay();
            r.Displays[0] = p.display;
        }
    }
}
