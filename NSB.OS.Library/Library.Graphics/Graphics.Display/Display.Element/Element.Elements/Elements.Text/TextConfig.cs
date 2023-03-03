
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

    public static TextConfig Centered = new TextConfig(TextOrientation.Horizontal, TextAlignment.Center);
    public static TextConfig Right = new TextConfig(TextOrientation.Horizontal, TextAlignment.Right);
    public static TextConfig CenteredVertical = new TextConfig(TextOrientation.Vertical, TextAlignment.Center);
    public static TextConfig RightVertical = new TextConfig(TextOrientation.Vertical, TextAlignment.Right);
    public static TextConfig Vertical = new TextConfig(TextOrientation.Vertical);
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
