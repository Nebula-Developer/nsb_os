using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;

namespace NSB.OS.Graphics.PanelNS;

public class Element {
    public int X { get; set; }
    public int Y { get; set; }

    public virtual void Draw(Display display) { }
}
