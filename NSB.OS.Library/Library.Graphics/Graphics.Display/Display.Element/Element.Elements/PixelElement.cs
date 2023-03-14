using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class PixelElement : Element {
    public char Character { get; set; }

    public PixelElement(int x, int y, char character, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) => Character = character;
    public PixelElement(int x, int y, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) => Character = ' ';
    public PixelElement(Vector2i position, char character, RGB? bg = null, RGB? fg = null) : base(position, bg, fg) => Character = character;
    public PixelElement(Vector2i position, RGB? bg = null, RGB? fg = null) : base(position, bg, fg) => Character = ' ';

    public override PixelMap Draw(PixelMap pixels) {
        pixels.SetRelativePixel(this, new Vector2i(0, 0), new Pixel(Character, BG, FG));
        return pixels;
    }
}