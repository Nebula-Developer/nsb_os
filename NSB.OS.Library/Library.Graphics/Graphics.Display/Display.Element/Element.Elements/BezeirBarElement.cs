
using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class BezeirBarElement : Element {
    public Vector2i start, end;
    public Vector2i[] points;
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public BezeirBarElement(int x, int y, int x2, int y2, Vector2i[]? points = null, RGB? bg = null, RGB? fg = null) {
        start = new Vector2i(x, y);
        end = new Vector2i(x2, y2);
        this.points = points ?? new Vector2i[0];
        BG = bg;
        FG = fg;
    }

    public BezeirBarElement(Vector2i startPos, Vector2i endPos, Vector2i[]? points = null, RGB? bg = null, RGB? fg = null) {
        start = startPos;
        end = endPos;
        this.points = points ?? new Vector2i[0];
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels) {
        for (int i = 0; i < pixels.Width; i++) {
            for (int j = 0; j < pixels.Height; j++) {
                if (i >= start.X && i <= end.X && j >= start.Y && j <= end.Y) {
                    pixels.SetPixel(this, new Vector2i(i, j), new Pixel(' ', BG, FG));
                }
            }
        }
        return pixels;
    }
}
