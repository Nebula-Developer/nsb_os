using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.PanelNS;

public class TextElement : Element {
    public string Text { get; set; }
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }


    public TextElement(int x, int y, string text, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        Text = text;
        BG = bg;
        FG = fg;
    }

    public override void Draw(Display display) {
        for (int i = 0; i < Text.Length; i++) {
            if (X + i < 0 || X + i >= display.Width) continue;
            display.SetPixel(new Vector2i(X + i, Y), new Pixel(Text[i], BG, FG));
        }
    }
}
