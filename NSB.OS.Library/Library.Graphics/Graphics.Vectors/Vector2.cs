
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

    public float Length() => (float)Math.Sqrt(X * X + Y * Y);
    public float Distance(Vector2 other) => (this - other).Length();
    public Vector2 Normalize() => this / Length();
    public Vector2 Rotate(float angle) {
        float rad = (float)(angle * Math.PI / 180);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        return new Vector2(X * cos - Y * sin, X * sin + Y * cos);
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator +(Vector2 a, float b) => new Vector2(a.X + b, a.Y + b);
    public static Vector2 operator +(float a, Vector2 b) => new Vector2(a + b.X, a + b.Y);

    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator -(Vector2 a) => new Vector2(-a.X, -a.Y);
    public static Vector2 operator -(Vector2 a, float b) => new Vector2(a.X - b, a.Y - b);
    public static Vector2 operator -(float a, Vector2 b) => new Vector2(a - b.X, a - b.Y);

    public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.X * b.X, a.Y * b.Y);
    public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.X * b, a.Y * b);
    public static Vector2 operator *(float a, Vector2 b) => new Vector2(a * b.X, a * b.Y);

    public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.X / b.X, a.Y / b.Y);
    public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.X / b, a.Y / b);
    public static Vector2 operator /(float a, Vector2 b) => new Vector2(a / b.X, a / b.Y);

    public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vector2 a, Vector2 b) => a.X != b.X || a.Y != b.Y;

    public override bool Equals(object? obj) => obj is Vector2 vector && this.X == vector.X && this.Y == vector.Y;
    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";
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

    public int Length() => (int)Math.Sqrt(X * X + Y * Y);
    public int Distance(Vector2i other) => (this - other).Length();
    public Vector2i Normalize() => this / Length();
    public Vector2i Rotate(int angle) {
        float rad = (float)(angle * Math.PI / 180);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        return new Vector2i((int)(X * cos - Y * sin), (int)(X * sin + Y * cos));
    }

    public static Vector2i operator +(Vector2i a, Vector2i b) => new Vector2i(a.X + b.X, a.Y + b.Y);
    public static Vector2i operator +(Vector2i a, int b) => new Vector2i(a.X + b, a.Y + b);
    public static Vector2i operator +(int a, Vector2i b) => new Vector2i(a + b.X, a + b.Y);

    public static Vector2i operator -(Vector2i a, Vector2i b) => new Vector2i(a.X - b.X, a.Y - b.Y);
    public static Vector2i operator -(Vector2i a) => new Vector2i(-a.X, -a.Y);
    public static Vector2i operator -(Vector2i a, int b) => new Vector2i(a.X - b, a.Y - b);
    public static Vector2i operator -(int a, Vector2i b) => new Vector2i(a - b.X, a - b.Y);

    public static Vector2i operator *(Vector2i a, Vector2i b) => new Vector2i(a.X * b.X, a.Y * b.Y);
    public static Vector2i operator *(Vector2i a, int b) => new Vector2i(a.X * b, a.Y * b);
    public static Vector2i operator *(int a, Vector2i b) => new Vector2i(a * b.X, a * b.Y);

    public static Vector2i operator /(Vector2i a, Vector2i b) => new Vector2i(a.X / b.X, a.Y / b.Y);
    public static Vector2i operator /(Vector2i a, int b) => new Vector2i(a.X / b, a.Y / b);
    public static Vector2i operator /(int a, Vector2i b) => new Vector2i(a / b.X, a / b.Y);

    public static bool operator ==(Vector2i a, Vector2i b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vector2i a, Vector2i b) => a.X != b.X || a.Y != b.Y;

    public override bool Equals(object? obj) => obj is Vector2i vector && this.X == vector.X && this.Y == vector.Y;
    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";
}
