using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Display home = new Display(new Vector2i(0, 0), new Vector2i(80, 25));
        Rectangle bg = new Rectangle(0, 0, 80, 25, new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(bg);
        TextElement t = new TextElement(0, 0, "NSB_OS", null, null);
        home.AddElement(t);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(home);
        renderer.Render();

        Console.ReadLine();
    }
}
