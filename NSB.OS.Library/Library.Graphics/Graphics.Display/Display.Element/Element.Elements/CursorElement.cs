using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class CursorElement : Element {
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public CursorElement(int x, int y, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        BG = bg;
        FG = fg;
    }

    public CursorElement(Vector2i position, RGB? bg = null, RGB? fg = null) {
        this.X = position.X;
        this.Y = position.Y;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        pixels.SetPixel(this, new Vector2i(0, 0), new Pixel('▒', BG, FG));
        return pixels;
    }
}
