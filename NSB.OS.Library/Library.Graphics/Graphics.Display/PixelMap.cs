using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Graphics.DisplayNS;

public class PixelMap {
    private Pixel[,] Pixels { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public PixelMap(int width, int height) {
        Width = width;
        Height = height;
        Pixels = new Pixel[Width, Height];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                Pixels[x, y] = new Pixel(' ', null, null);
    }

    public void SetPixel(Element element, Vector2i position, Pixel pixel) {
        if (element.X + position.X < 0 || element.X + position.X >= Width) return;
        if (element.Y + position.Y < 0 || element.Y + position.Y >= Height) return;
        Pixels[element.X + position.X, element.Y + position.Y] = pixel;
    }

    public void SetPixel(Vector2i position, Pixel pixel) {
        if (position.X < 0 || position.X >= Width) return;
        if (position.Y < 0 || position.Y >= Height) return;
        Pixels[position.X, position.Y] = pixel;
    }

    public Pixel GetPixel(Vector2i position) => Pixels[position.X, position.Y];
    public Pixel GetPixel(int x, int y) => Pixels[x, y];
}