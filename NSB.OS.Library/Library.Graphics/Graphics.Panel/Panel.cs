using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.PanelNS;

public class Panel {
    public int Width { get; set; }
    public int Height { get; set; }

    public int ViewX { get; set; }
    public int ViewY { get; set; }

    public Display display;
    public List<Element> Elements { get; set; }

    public void AddElement(Element element) {
        Elements.Add(element);
    }

    public void RemoveElement(Element element) {
        Elements.Remove(element);
    }

    public Panel(Vector2i position, Vector2i size, Display display) {
        ViewX = position.X;
        ViewY = position.Y;
        Width = size.X;
        Height = size.Y;
        this.display = display;
        Elements = new List<Element>();
    }

    public void UpdateDisplay() {
        display.ViewX = ViewX;
        display.ViewY = ViewY;
        display.Width = Width;
        display.Height = Height;
        display.AllocatePixels();

        foreach (Element element in Elements) {
            element.Draw(display);
        }
        
        display.Update();
    }
}
