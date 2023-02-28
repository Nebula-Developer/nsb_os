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

    public void SetPixel(Vector2i position, Pixel pixel) => Pixels[position.X, position.Y] = pixel;
    public void SetPixel(int x, int y, Pixel pixel) => Pixels[x, y] = pixel;

    public Pixel GetPixel(Vector2i position) => Pixels[position.X, position.Y];
    public Pixel GetPixel(int x, int y) => Pixels[x, y];
}