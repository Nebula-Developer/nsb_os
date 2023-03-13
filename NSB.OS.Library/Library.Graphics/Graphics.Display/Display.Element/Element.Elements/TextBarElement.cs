using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class CharBarElement : Element {
    public Vector2i start, end;
    public char Character { get; set; }


    public CharBarElement(int x, int y, int x2, int y2, char character, RGB? bg = null, RGB? fg = null) : base(x, y, bg, fg) {
        start = new Vector2i(x, y);
        end = new Vector2i(x2, y2);
        Character = character;
    }

    public CharBarElement(Vector2i startPos, Vector2i endPos, char character, RGB? bg = null, RGB? fg = null) : base(startPos, bg, fg) {
        start = startPos;
        end = endPos;
        Character = character;
    }

    public override PixelMap Draw(PixelMap pixels) {
        int x0 = start.X;
        int y0 = start.Y;
        int x1 = end.X;
        int y1 = end.Y;

        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int err = dx - dy;

        while (true) {
            pixels.SetRelativePixel(this, new Vector2i(x0, y0), new Pixel(Character, BG, FG));

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
