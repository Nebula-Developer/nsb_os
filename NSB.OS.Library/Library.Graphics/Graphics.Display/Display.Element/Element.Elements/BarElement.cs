using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class BarElement : Element {
    public Vector2i end;

    public BarElement(int x, int y, int x2, int y2, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) => end = new Vector2i(x2, y2);
    public BarElement(Vector2i startPos, Vector2i endPos, RGB? bg = null, RGB? fg = null) : base(startPos, bg, fg) => end = endPos;

    public override PixelMap Draw(PixelMap pixels) {
        int x0 = this.X;
        int y0 = this.Y;
        int x1 = end.X;
        int y1 = end.Y;

        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int err = dx - dy;

        while (true) {
            pixels.SetPixel(new Vector2i(x0, y0), new Pixel(' ', BG, FG));

            if (x0 == x1 && y0 == y1) {
                break;
            }

            int e2 = 2 * err;
            if (e2 > -dy) {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx) {
                err += dx;
                y0 += sy;
            }
        }

        return pixels;
    }
}
