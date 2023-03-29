using System;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace Programs.GraphicTest;

public static class Program {
    public static void Run() {
        Display display = new(new Vector2i(0, 0), new Vector2i(80, 25));
        PixelElement[,] points = new PixelElement[80, 25];
        
        for (int x = 0; x < 80; x++) {
            for (int y = 0; y < 25; y++) {
                int rgb = (int)(x * y * 0.5) % 255;
                points[x, y] = new PixelElement(x, y, new RGB(0, rgb, rgb), new RGB(255, 255, 255));
                display.AddElement(points[x, y]);
            }
        }

        RendererStack renderer = new();
        renderer.AddDisplay(display);
        renderer.Render(true);
    }
}
 