using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class TextElement : Element {
    public TextConfig Config { get; set; }
    public string Text { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public TextElement(int x, int y, string text, TextConfig? config, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Text = text;
        Config = config ?? new TextConfig();
        BG = bg;
        FG = fg;
    }

    public TextElement(Vector2i pos, string text, TextConfig? config, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        Text = text;
        Config = config ?? new TextConfig();
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        TextOrientation orientation = Config.Orientation;

        switch (orientation) {
            case TextOrientation.Horizontal:
                return DrawHorizontal(pixels);
            case TextOrientation.Vertical:
                return DrawVertical(pixels);
        }

        return pixels;
    }

    private PixelMap DrawHorizontal(PixelMap pixels) {
        TextAlignment alignment = Config.Alignment;
        int width = Config.Width == -1 ? pixels.Width : Config.Width;
        int start = 0;
        switch (alignment) {
            case TextAlignment.Left:
                start = 0;
                break;
            case TextAlignment.Center:
                start = (width - Text.Length) / 2;
                break;
            case TextAlignment.Right:
                start = width - Text.Length;
                break;
        }
        for (int i = 0; i < Text.Length; i++) {
            if (X + i < 0 || X + i >= pixels.Width) continue;
            pixels.SetPixel(this, new Vector2i(X + i + start, 0), new Pixel(Text[i], BG, FG));
        }
        return pixels;
    }

    private PixelMap DrawVertical(PixelMap pixels) {
        TextAlignment alignment = Config.Alignment;
        int height = Config.Height == -1 ? pixels.Height : Config.Height;
        int start = 0;
        switch (alignment) {
            case TextAlignment.Left:
                start = 0;
                break;
            case TextAlignment.Center:
                start = (height - Text.Length) / 2;
                break;
            case TextAlignment.Right:
                start = height - Text.Length;
                break;
        }
        for (int i = 0; i < Text.Length; i++) {
            if (Y + i < 0 || Y + i >= pixels.Height) continue;
            pixels.SetRelativePixel(this, new Vector2i(0, Y + i + start), new Pixel(Text[i], BG, FG));
        }
        return pixels;
    }
}
