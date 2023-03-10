using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Graphics.DisplayNS;

public class Display
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int ViewX { get; set; }
    public int ViewY { get; set; }

    public List<Element> Elements { get; set; }

    public void AddElement(Element element)
    {
        Elements.Add(element);
    }

    public void RemoveElement(Element element)
    {
        Elements.Remove(element);
    }

    public Display(Vector2i position, Vector2i size)
    {
        ViewX = position.X;
        ViewY = position.Y;
        Width = size.X;
        Height = size.Y;
        Update = () => { };
        Elements = new List<Element>();
    }

    public Action Update { get; set; }

    public PixelMap AllocPixels()
    {
        PixelMap Pixels = new PixelMap(Width, Height);
        return Pixels;
    }

    public PixelMap GetPixels()
    {
        PixelMap Pixels = AllocPixels();
        foreach (Element element in Elements)
        {
            Pixels = element.Draw(Pixels);
        }
        return Pixels;
    }
}
