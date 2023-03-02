using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class Rectangle : Element {
    public int Width { get; set; }
    public int Height { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public Rectangle(int x, int y, int width, int height, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Width = width;
        Height = height;
        BG = bg;
        FG = fg;
    }

    public Rectangle(Vector2i pos, int width, int height, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        Width = width;
        Height = height;
        BG = bg;
        FG = fg;
    }
    
    public override PixelMap Draw(PixelMap pixels) {
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                pixels.SetPixel(this, new Vector2i(x, y), new Pixel(' ', BG, FG));
            }
        }
        return pixels;
    }
}
