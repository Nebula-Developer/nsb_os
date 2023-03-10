using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;

namespace NSB.OS.Graphics.DisplayNS;

public class Element {
    public int X { get; set; }
    public int Y { get; set; }
    public bool Visible = true;

    public virtual PixelMap Draw(PixelMap pixels) { return pixels; }
}
