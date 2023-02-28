using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.PanelNS;

public class Rectangle : Element {
    public int Width { get; set; }
    public int Height { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public Rectangle(int x, int y, int width, int height, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Width = width;
        Height = height;
        BG = bg;
        FG = fg;
    }

    public override void Draw(Display display) {
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                display.SetPixel(new Vector2i(X + x, Y + y), new Pixel(' ', BG, FG));
            }
        }
    }
}
