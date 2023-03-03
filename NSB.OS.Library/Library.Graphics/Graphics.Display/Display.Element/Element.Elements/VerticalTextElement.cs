using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class VerticalTextElement : Element {
    public string Text { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public VerticalTextElement(int x, int y, string text, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Text = text;
        BG = bg;
        FG = fg;
    }

    public VerticalTextElement(Vector2i pos, string text, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        Text = text;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        for (int i = 0; i < Text.Length; i++) {
            if (Y + i < 0 || Y + i >= pixels.Height) continue;
            pixels.SetPixel(new Vector2i(X, Y + i), new Pixel(Text[i], BG, FG));
        }
        return pixels;
    }
}
