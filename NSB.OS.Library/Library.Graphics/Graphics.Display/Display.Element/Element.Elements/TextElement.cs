using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class TextElement : Element {
    public string Text { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public TextElement(int x, int y, string text, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Text = text;
        BG = bg;
        FG = fg;
    }

    public TextElement(Vector2i pos, string text, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        Text = text;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        for (int i = 0; i < Text.Length; i++) {
            if (X + i < 0 || X + i >= pixels.Width) continue;
            pixels.SetPixel(this, new Vector2i(X + i, Y), new Pixel(Text[i], BG, FG));
        }
        return pixels;
    }
}
