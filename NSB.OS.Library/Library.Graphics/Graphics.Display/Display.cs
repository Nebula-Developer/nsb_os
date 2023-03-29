using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;
using NSB.OS.Logic.Variables;

namespace NSB.OS.Graphics.DisplayNS;

public class Display {
    public DependentVariable Width { get; set; }
    public DependentVariable Height { get; set; }

    public int ViewX { get; set; }
    public int ViewY { get; set; }

    public List<Element> Elements { get; set; }

    public void AddElement(Element element) {
        Elements.Add(element);
    }

    public void RemoveElement(Element element) {
        Elements.Remove(element);
    }

    public Display(Vector2i position, Vector2i size) {
        ViewX = position.X;
        ViewY = position.Y;
        Width = new DependentVariable(size.X);
        Height = new DependentVariable(size.Y);
        Elements = new List<Element>();
    }

    public virtual void Update() { }

    public PixelMap AllocPixels() {
        PixelMap Pixels = new(Width, Height);
        return Pixels;
    }

    public PixelMap GetPixels() {
        PixelMap Pixels = AllocPixels();
        foreach (Element element in Elements) {
            if (!element.Visible) continue;
            Pixels = element.Draw(Pixels);
        }
        return Pixels;
    }
}
