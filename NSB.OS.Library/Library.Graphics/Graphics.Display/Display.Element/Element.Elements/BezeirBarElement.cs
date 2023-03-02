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

    public override PixelMap Draw(PixelMap pixels)
    {
        float t = 0;
        Vector2 old = new Vector2(start.X, start.Y);
        Vector2[] newPoints = new Vector2[points.Length + 1];
        newPoints[0] = new Vector2(start.X, start.Y);
        for (int i = 1; i < newPoints.Length; i++)
        {
            newPoints[i] = new Vector2(points[i - 1].X, points[i - 1].Y);
        }

        while (t <= 1)
        {
            Vector2 current = new Vector2(newPoints[0].X, newPoints[0].Y) * (float)Math.Pow(1 - t, newPoints.Length) * (float)Math.Pow(t, 0);
            for (int i = 1; i < newPoints.Length; i++)
            {
                current += new Vector2(newPoints[i].X, newPoints[i].Y) * (float)Math.Pow(1 - t, newPoints.Length - i) * (float)Math.Pow(t, i);
            }
            current += new Vector2(end.X, end.Y) * (float)Math.Pow(1 - t, newPoints.Length) * (float)Math.Pow(t, 0);
            Vector2i currenti = new Vector2i((int)current.X, (int)current.Y);
            pixels.SetPixel(this, currenti, new Pixel(' ', BG, FG));
            t += 0.01f;
        }
        return pixels;
    }
}
