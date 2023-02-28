using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class BarElement : Element {
    public Vector2i start, end;
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public BarElement(int x, int y, int x2, int y2, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        start = new Vector2i(x, y);
        end = new Vector2i(x2, y2);
        BG = bg;
        FG = fg;
    }

    public BarElement(Vector2i startPos, Vector2i endPos, RGB? bg = null, RGB? fg = null) {
        this.X = startPos.X;
        this.Y = startPos.Y;
        start = startPos;
        end = endPos;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        for (int i = 0; i < pixels.Width; i++) {
            for (int j = 0; j < pixels.Height; j++) {
                if (i >= start.X && i <= end.X && j >= start.Y && j <= end.Y) {
                    pixels.SetPixel(new Vector2i(i, j), new Pixel(' ', BG, FG));
                }
            }
        }
        return pixels;
    }
}
