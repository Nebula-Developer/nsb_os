using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class Element {
    public int X { get; set; }
    public int Y { get; set; }

    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public bool Visible = true;

    public virtual PixelMap Draw(PixelMap pixels) { return pixels; }

    public Element(int x, int y, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        this.BG = bg;
        this.FG = fg;
    }

    public Element(Vector2i position, RGB? bg = null, RGB? fg = null) {
        this.X = position.X;
        this.Y = position.Y;
        this.BG = bg;
        this.FG = fg;
    }
}
