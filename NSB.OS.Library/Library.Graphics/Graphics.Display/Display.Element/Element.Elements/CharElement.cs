using NSB.OS.Graphics.Mathematics;

namespace NSB.OS.Graphics.DisplayNS;

public class CharElement : Element
{
    public char Character;
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public CharElement(int x, int y, char character, RGB? bg = null, RGB? fg = null)
    {
        this.X = x;
        this.Y = y;
        Character = character;
        BG = bg;
        FG = fg;
    }

    public CharElement(Vector2i position, char character, RGB? bg = null, RGB? fg = null)
    {
        this.X = position.X;
        this.Y = position.Y;
        Character = character;
        BG = bg;
        FG = fg;
    }

    public override PixelMap Draw(PixelMap pixels)
    {
        pixels.SetPixel(this, new Vector2i(0, 0), new Pixel(Character, BG, FG));
        return pixels;
    }
}
