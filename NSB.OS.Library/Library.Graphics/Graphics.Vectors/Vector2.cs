
namespace NSB.OS.Graphics.Mathematics;

public class Vector2 {
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2() {
        X = 0;
        Y = 0;
    }

    public Vector2(float x, float y) {
        X = x;
        Y = y;
    }
}

public class Vector2i {
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2i() {
        X = 0;
        Y = 0;
    }

    public Vector2i(int x, int y) {
        X = x;
        Y = y;
    }

    public Vector2i ToCharSquare() {
        this.X = this.X * 2;
        return this;
    }
}
