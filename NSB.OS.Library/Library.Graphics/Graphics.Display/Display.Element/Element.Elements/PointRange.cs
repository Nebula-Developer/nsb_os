using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class PointRange : Element {
    public List<PointElement> Points { get; set; }

    public PointRange(int x, int y, List<PointElement>? Points) {
        this.X = x;
        this.Y = y;
        this.Points = Points ?? new List<PointElement>(0);
    }

    public PointRange(Vector2i position, List<PointElement>? Points) {
        this.X = position.X;
        this.Y = position.Y;
        this.Points = Points ?? new List<PointElement>(0);
    }

    public override PixelMap Draw(PixelMap pixels) {
        foreach (PointElement point in Points) pixels = point.Draw(pixels);
        return pixels;
    }
}
