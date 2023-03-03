using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        int width = 80;
        int height = Console.WindowHeight;
        Console.CursorVisible = false;
        Console.Clear();

        Display home = new Display(new Vector2i(0, 0), new Vector2i(width, height));
        Rectangle bg = new Rectangle(0, 0, width, height, new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(bg);
        CenteredTextElement t = new CenteredTextElement(0, 1, "NSB_OS", width, null, null);
        home.AddElement(t);
        OutlineElement o = new OutlineElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(o);
        PointElement cursor = new PointElement(0, 0, new RGB(255, 0, 0));
        home.AddElement(cursor);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(home);
        renderer.Render();

        while (true) {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.W) cursor.Y--;
            if (key.Key == ConsoleKey.S) cursor.Y++;
            if (key.Key == ConsoleKey.A) cursor.X--;
            if (key.Key == ConsoleKey.D) cursor.X++;

            t.Text = $"X: {cursor.X}, Y: {cursor.Y}";

            renderer.Render();
        }
    }
}
