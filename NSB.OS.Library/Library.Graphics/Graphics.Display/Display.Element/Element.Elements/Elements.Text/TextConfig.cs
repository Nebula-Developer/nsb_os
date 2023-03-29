
namespace NSB.OS.Graphics.DisplayNS;

public class TextConfig {
    public TextOrientation Orientation { get; set; }
    public TextAlignment Alignment { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public TextConfig(TextOrientation orientation = TextOrientation.Horizontal, TextAlignment alignment = TextAlignment.Left, int width = -1, int height = -1) {
        Orientation = orientation;
        Alignment = alignment;
        Width = width;
        Height = height;
    }

    public static TextConfig Centered = new(TextOrientation.Horizontal, TextAlignment.Center);
    public static TextConfig Right = new(TextOrientation.Horizontal, TextAlignment.Right);
    public static TextConfig CenteredVertical = new(TextOrientation.Vertical, TextAlignment.Center);
    public static TextConfig RightVertical = new(TextOrientation.Vertical, TextAlignment.Right);
    public static TextConfig Vertical = new(TextOrientation.Vertical);
}

public enum TextOrientation {
    Horizontal,
    Vertical
}

public enum TextAlignment {
    Left,
    Center,
    Right
}
