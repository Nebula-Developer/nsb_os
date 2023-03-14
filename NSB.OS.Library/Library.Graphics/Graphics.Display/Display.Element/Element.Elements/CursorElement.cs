using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class CursorElement : Element {
    public CursorElement(int x, int y, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) { }

    public CursorElement(Vector2i position, RGB? bg = null, RGB? fg = null) : base(position, bg, fg) { }

    public override PixelMap Draw(PixelMap pixels) {
        pixels.SetRelativePixel(this, new Vector2i(0, 0), new Pixel('▒', BG, FG));
        return pixels;
    }
}
