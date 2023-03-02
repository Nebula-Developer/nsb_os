
namespace NSB.OS.Graphics.Mathematics;

public class Vector3 {
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vector3() {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public Vector3(float x, float y, float z) {
        X = x;
        Y = y;
        Z = z;
    }

    public int Length() => (int)Math.Sqrt(X * X + Y * Y + Z * Z);
    public float Distance(Vector3 other) => (this - other).Length();
    public Vector3 Normalize() => this / Length();
    public Vector3 Rotate(float angle) {
        float rad = (float)(angle * Math.PI / 180);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        return new Vector3(X * cos - Y * sin, X * sin + Y * cos, Z);
    }

    public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator +(Vector3 a, float b) => new Vector3(a.X + b, a.Y + b, a.Z + b);
    public static Vector3 operator +(float a, Vector3 b) => new Vector3(a + b.X, a + b.Y, a + b.Z);

    public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3 operator -(Vector3 a) => new Vector3(-a.X, -a.Y, -a.Z);
    public static Vector3 operator -(Vector3 a, float b) => new Vector3(a.X - b, a.Y - b, a.Z - b);
    public static Vector3 operator -(float a, Vector3 b) => new Vector3(a - b.X, a - b.Y, a - b.Z);

    public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(float a, Vector3 b) => new Vector3(a * b.X, a * b.Y, a * b.Z);

    public static Vector3 operator /(Vector3 a, Vector3 b) => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    public static Vector3 operator /(Vector3 a, float b) => new Vector3(a.X / b, a.Y / b, a.Z / b);
    public static Vector3 operator /(float a, Vector3 b) => new Vector3(a / b.X, a / b.Y, a / b.Z);

    public static bool operator ==(Vector3 a, Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);

    public override bool Equals(object? obj) => obj is Vector3 vector && this == vector;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    public override string ToString() => $"({X}, {Y}, {Z})";
}

public class Vector3i {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector3i() {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public Vector3i(int x, int y, int z) {
        X = x;
        Y = y;
        Z = z;
    }

    public int Length() => (int)Math.Sqrt(X * X + Y * Y + Z * Z);
    public float Distance(Vector3i other) => (this - other).Length();
    public Vector3i Normalize() => this / Length();
    public Vector3i Rotate(float angle) {
        float rad = (float)(angle * Math.PI / 180);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        return new Vector3i((int)(X * cos - Y * sin), (int)(X * sin + Y * cos), Z);
    }

    public static Vector3i operator +(Vector3i a, Vector3i b) => new Vector3i(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3i operator +(Vector3i a, int b) => new Vector3i(a.X + b, a.Y + b, a.Z + b);
    public static Vector3i operator +(Vector3i a, float b) => new Vector3i(a.X + (int)b, a.Y + (int)b, a.Z + (int)b);
    public static Vector3i operator +(int a, Vector3i b) => new Vector3i(a + b.X, a + b.Y, a + b.Z);

    public static Vector3i operator -(Vector3i a, Vector3i b) => new Vector3i(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3i operator -(Vector3i a) => new Vector3i(-a.X, -a.Y, -a.Z);
    public static Vector3i operator -(Vector3i a, int b) => new Vector3i(a.X - b, a.Y - b, a.Z - b);
    public static Vector3i operator -(Vector3i a, float b) => new Vector3i(a.X - (int)b, a.Y - (int)b, a.Z - (int)b);
    public static Vector3i operator -(int a, Vector3i b) => new Vector3i(a - b.X, a - b.Y, a - b.Z);

    public static Vector3i operator *(Vector3i a, Vector3i b) => new Vector3i(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3i operator *(Vector3i a, int b) => new Vector3i(a.X * b, a.Y * b, a.Z * b);
    public static Vector3i operator *(Vector3i a, float b) => new Vector3i(a.X * (int)b, a.Y * (int)b, a.Z * (int)b);
    public static Vector3i operator *(int a, Vector3i b) => new Vector3i(a * b.X, a * b.Y, a * b.Z);
    public static Vector3i operator *(float a, Vector3i b) => new Vector3i((int)a * b.X, (int)a * b.Y, (int)a * b.Z);

    public static Vector3i operator /(Vector3i a, Vector3i b) => new Vector3i(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    public static Vector3i operator /(Vector3i a, int b) => new Vector3i(a.X / b, a.Y / b, a.Z / b);
    public static Vector3i operator /(Vector3i a, float b) => new Vector3i(a.X / (int)b, a.Y / (int)b, a.Z / (int)b);
    public static Vector3i operator /(int a, Vector3i b) => new Vector3i(a / b.X, a / b.Y, a / b.Z);
    public static Vector3i operator /(float a, Vector3i b) => new Vector3i((int)a / b.X, (int)a / b.Y, (int)a / b.Z);

    public static bool operator ==(Vector3i a, Vector3i b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    public static bool operator !=(Vector3i a, Vector3i b) => !(a == b);

    public override bool Equals(object? obj) => obj is Vector3i vector && this == vector;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    public override string ToString() => $"({X}, {Y}, {Z})";
}
