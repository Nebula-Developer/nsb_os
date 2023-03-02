
namespace NSB.OS.Graphics;

public class RGB {
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }

    public RGB() {
        R = 0;
        G = 0;
        B = 0;
    }

    public RGB(int r, int g, int b) {
        R = r;
        G = g;
        B = b;
    }

    public string ToBGESC() {
        return $"\x1b[48;2;{R};{G};{B}m";
    }

    public string ToFGESC() {
        return $"\x1b[38;2;{R};{G};{B}m";
    }

    public override string ToString() {
        return $"({R}, {G}, {B})";
    }

    public RGB Lerp(RGB other, float t) {
        return new RGB(
            (int)(R + (other.R - R) * t),
            (int)(G + (other.G - G) * t),
            (int)(B + (other.B - B) * t)
        );
    }

    public static RGB operator +(RGB a, RGB b) => new RGB(a.R + b.R, a.G + b.G, a.B + b.B);
    public static RGB operator -(RGB a, RGB b) => new RGB(a.R - b.R, a.G - b.G, a.B - b.B);
    public static RGB operator *(RGB a, RGB b) => new RGB(a.R * b.R, a.G * b.G, a.B * b.B);
    public static RGB operator /(RGB a, RGB b) => new RGB(a.R / b.R, a.G / b.G, a.B / b.B);
    public static RGB operator %(RGB a, RGB b) => new RGB(a.R % b.R, a.G % b.G, a.B % b.B);
}
