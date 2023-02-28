using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Graphics.DisplayNS;

public class Display {
    public int Width { get; set; }
    public int Height { get; set; }

    public int ViewX { get; set; }
    public int ViewY { get; set; }

    public List<List<Pixel>> Pixels = new List<List<Pixel>>();

    public Display(Vector2i position, Vector2i size) {
        ViewX = position.X;
        ViewY = position.Y;
        Width = size.X;
        Height = size.Y;
        Update = () => { };
        AllocatePixels();
    }

    public void AllocatePixels() {
        for (int y = 0; y < Height; y++) {
            if (Pixels.Count <= y) Pixels.Add(new List<Pixel>());
            for (int x = 0; x < Width; x++) {
                if (Pixels[y].Count <= x) Pixels[y].Add(new Pixel());
            }
        }
    }

    public Action Update { get; set; }

    public void SetPixel(Vector2i position, Pixel pixel) {
        if (Pixels.Count <= position.Y || Pixels[position.Y].Count <= position.X) {
            AllocatePixels();
        }

        if (position.X < 0 || position.Y < 0 || position.X >= Width || position.Y >= Height) return;
        Pixels[position.Y][position.X] = pixel;
    }

    public void SetString(Vector2i position, string str, RGB? bg = null, RGB? fg = null) {
        for (int i = 0; i < str.Length; i++) {
            SetPixel(new Vector2i(position.X + i, position.Y), new Pixel(str[i], bg, fg));
        }
    }
}

public class Pixel {
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }
    public char? Character { get; set; }

    public Pixel(char? character = null, RGB? bg = null, RGB? fg = null) {
        Character = character;
        BG = bg;
        FG = fg;
    }
}

public class Renderer {
    public List<Display> Displays { get; set; } = new List<Display>();

    public string GetDisplayStr() {
        List<List<Pixel>> displayStrs = new List<List<Pixel>>();

        foreach (Display display in Displays) {
            display.Update?.Invoke();
            int yStart = display.ViewY < 0 ? -display.ViewY : 0;
            int xStart = display.ViewX < 0 ? -display.ViewX : 0;
            for (int y = yStart; y < display.Height; y++) {
                if (displayStrs.Count <= y + display.ViewY) while (displayStrs.Count <= y + display.ViewY) displayStrs.Add(new List<Pixel>());
                for (int x = xStart; x < display.Width; x++) {
                    int locX = x + display.ViewX;
                    int locY = y + display.ViewY;
                    if (displayStrs[locY].Count <= locX) 
                        while (displayStrs[locY].Count <= locX) displayStrs[locY].Add(new Pixel(' '));
                    if (display.Pixels[y][x].Character != null) displayStrs[locY][locX].Character = display.Pixels[y][x].Character;
                    if (display.Pixels[y][x].BG != null) displayStrs[locY][locX].BG = display.Pixels[y][x].BG;
                    if (display.Pixels[y][x].FG != null) displayStrs[locY][locX].FG = display.Pixels[y][x].FG;
                }
            }
        }

        string str = "";
        foreach (List<Pixel> displayStr in displayStrs) {
            foreach (Pixel pixel in displayStr) {
                if (pixel.BG != null) {
                    str += pixel.BG?.ToBGESC();
                }
                if (pixel.FG != null) {
                    str += pixel.FG?.ToFGESC();
                }

                if (pixel.Character != null) {
                    str += pixel.Character;
                } else {
                    // Red G
                    str += " ";
                }
                str += "\x1b[0m";
            }
            str += "\n";
        }

        return str;
    }

    public void Render() {
        Console.Write("\x1b[2J\x1b[0;0H" + GetDisplayStr());
    }

    public Renderer(params Display[] displays) {
        Displays.AddRange(displays);
    }

    Thread? renderThread;
    Action? renderKill;

    public int RenderFrequency { get; set; } = 30;
    public bool UseIdealRenderFrequency { get; set; } = true;
    public bool UseThreadedRender { get; set; } = true;

    public bool IsRendering = false;

    public void StartRenderThread(bool overrideThread = false) {
        IsRendering = true;

        if (renderThread != null) {
            if (overrideThread)
                renderKill?.Invoke();
            else return;
        }

        DateTime pastRender = DateTime.Now;

        renderThread = new System.Threading.Thread(() => {
            bool isLoopRendering = true;
            while (isLoopRendering) {
                if (UseThreadedRender) {
                    new System.Threading.Thread(() => {
                        Render();
                    }).Start();
                } else {
                    Render();
                }

                if (UseIdealRenderFrequency) {
                    double time = (DateTime.Now - pastRender).TotalMilliseconds;
                    if (((1000 / RenderFrequency) - time) < 0) time = 0;
                    System.Threading.Thread.Sleep((int)(1000 / RenderFrequency - time));
                    pastRender = DateTime.Now;
                } else System.Threading.Thread.Sleep(1000 / RenderFrequency);
                
                renderKill = () => {
                    isLoopRendering = false;
                };
            }
        });
        renderThread.Start();
    }

    public void StopRenderThread() {
        renderKill?.Invoke();
        renderThread = null;
        IsRendering = false;
    }
}
