using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class CenteredTextElement : Element {
    public string Text { get; set; }
    public int Width = 0;
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public CenteredTextElement(int x, int y, string text, int width, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Text = text;
        Width = width;
        BG = bg;
        FG = fg;
    }

    public CenteredTextElement(Vector2i pos, string text, int width, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        Text = text;
        Width = width;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        int start = (Width - Text.Length) / 2;
        for (int i = 0; i < Text.Length; i++) {
            if (X + i < 0 || X + i >= pixels.Width) continue;
            pixels.SetPixel(this, new Vector2i(X + i + start, Y), new Pixel(Text[i], BG, FG));
        }
        return pixels;
    }
}
