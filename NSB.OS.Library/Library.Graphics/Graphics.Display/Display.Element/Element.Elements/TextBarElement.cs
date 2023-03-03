using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class TextBarElement : Element {
    public Vector2i start, end;
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }
    public char Character { get; set; }


    public TextBarElement(int x, int y, int x2, int y2, char character, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        start = new Vector2i(x, y);
        end = new Vector2i(x2, y2);
        Character = character;
        BG = bg;
        FG = fg;
    }

    public TextBarElement(Vector2i startPos, Vector2i endPos, char character, RGB? bg = null, RGB? fg = null) {
        this.X = startPos.X;
        this.Y = startPos.Y;
        start = startPos;
        end = endPos;
        Character = character;
        BG = bg;
        FG = fg;
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
            pixels.SetPixel(new Vector2i(x0, y0), new Pixel(Character, BG, FG));

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
