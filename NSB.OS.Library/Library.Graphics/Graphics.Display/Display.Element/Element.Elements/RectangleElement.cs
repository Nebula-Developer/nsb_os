using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class RectangleElement : Element {
    public int Width { get; set; }
    public int Height { get; set; }

    public RectangleElement(int x, int y, int width, int height, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) {
        Width = width;
        Height = height;
    }

    public RectangleElement(Vector2i pos, int width, int height, RGB? bg = null, RGB? fg = null) : base(pos, bg, fg) {
        Width = width;
        Height = height;
    }

    public override PixelMap Draw(PixelMap pixels) {
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                pixels.SetRelativePixel(this, new Vector2i(x, y), new Pixel(' ', BG, FG));
            }
        }
        return pixels;
    }
}
