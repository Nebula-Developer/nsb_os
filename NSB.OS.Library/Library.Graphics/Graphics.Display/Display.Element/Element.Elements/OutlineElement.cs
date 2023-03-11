using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class OutlineElement : Element {
    public int Width { get; set; }
    public int Height { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public char tl = '┌';
    public char tr = '┐';
    public char bl = '└';
    public char br = '┘';
    public char h = '─';
    public char v = '│';

    public OutlineElement(int x, int y, int width, int height, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Width = width;
        Height = height;
        BG = bg;
        FG = fg;
    }

    public OutlineElement(Vector2i pos, int width, int height, RGB? bg = null, RGB? fg = null) {
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
                if (x == 0 && y == 0) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(tl, BG, FG));
                } else if (x == Width - 1 && y == 0) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(tr, BG, FG));
                } else if (x == 0 && y == Height - 1) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(bl, BG, FG));
                } else if (x == Width - 1 && y == Height - 1) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(br, BG, FG));
                } else if (x == 0 || x == Width - 1) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(v, BG, FG));
                } else if (y == 0 || y == Height - 1) {
                    pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(h, BG, FG));
                }
            }
        }
        return pixels;
    }
}
