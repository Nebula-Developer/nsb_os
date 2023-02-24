
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
}
