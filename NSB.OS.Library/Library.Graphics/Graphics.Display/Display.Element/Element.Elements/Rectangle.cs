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

    public override PixelMap Draw(PixelMap pixels) {
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                if (X + x < 0 || X + x >= pixels.Width || Y + y < 0 || Y + y >= pixels.Height) continue;
                pixels.SetPixel(new Vector2i(X + x, Y + y), new Pixel(' ', BG, FG));
            }
        }
        return pixels;
    }
}
