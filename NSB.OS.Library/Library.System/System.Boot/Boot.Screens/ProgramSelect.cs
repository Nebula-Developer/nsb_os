using NSB.OS.FileSystem;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.ProgramsNS;
using NSB.OS.SystemNS.InputNS;

namespace NSB.OS.SystemNS.BootNS.BootScreens;

public class ProgramSelect : Display {
    private RectangleElement Background;
    private OutlineElement Outline;
    private TextElement Title;
    public CursorElement Cursor;
    private RendererStack Renderer;

    public ProgramSelect(Vector2i position, Vector2i size, RendererStack renderer) : base(position, size) {
        Background = new RectangleElement(0, 0, this.Width, this.Height, new RGB(0, 0, 0), new RGB(0, 0, 0));
        Outline = new OutlineElement(0, 0, this.Width, this.Height, null, new RGB(255, 255, 255));
        Title = new TextElement(0, 0, "[ Program Select ]", TextConfig.Centered, null, new RGB(255, 255, 255));
        Cursor = new CursorElement(1, 1, null, new RGB(255, 255, 255));

        this.AddElement(Background);
        this.AddElement(Outline);
        this.AddElement(Title);
        this.AddElement(Cursor);

        Input.AddKeyAction(OnKeyPress);
        this.Renderer = renderer;
    }

    public void OnKeyPress() {
        if (!Input.KeyAvailable) return;
        Input.KeyAvailable = false;

        ConsoleKeyInfo key = Input.Key;

        if (key.Key == ConsoleKey.W) {
            if (Cursor.Y > 1) Cursor.Y--;
        } else if (key.Key == ConsoleKey.S) {
            if (Cursor.Y < this.Height - 2) Cursor.Y++;
        } else if (key.Key == ConsoleKey.A) {
            if (Cursor.X > 1) Cursor.X--;
        } else if (key.Key == ConsoleKey.D) {
            if (Cursor.X < this.Width - 2) Cursor.X++;
        } else if (key.Key == ConsoleKey.Enter) {

        }

        Renderer.Render();
    }
}