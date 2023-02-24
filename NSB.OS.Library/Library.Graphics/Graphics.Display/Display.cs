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
    public int RenderFrequency { get; set; } = 30;
    public bool UseIdealRenderFrequency { get; set; } = true;
    public bool UseThreadedRender { get; set; } = true;

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
                if (Pixels[y].Count <= x) Pixels[y].Add(new Pixel(' '));
            }
        }
    }

    public Action Update { get; set; }

    public void SetPixel(Vector2i position, Pixel pixel) {
        if (Pixels.Count <= position.Y || Pixels[position.Y].Count <= position.X) {
            AllocatePixels();
        }
        Pixels[position.Y][position.X] = pixel;
    }
}

public class Pixel {
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }
    public char Character { get; set; }

    public Pixel(char character, RGB? bg = null, RGB? fg = null) {
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
            for (int y = 0; y < display.Height; y++) {
                if (displayStrs.Count <= y) displayStrs.Add(new List<Pixel>());
                for (int x = 0; x < display.Width; x++) {
                    if (displayStrs[y].Count <= x) displayStrs[y].Add(new Pixel(' '));
                    displayStrs[y][x] = display.Pixels[y][x];
                }

                if (displayStrs[y].Count < display.Width) {
                    for (int i = displayStrs[y].Count; i < display.Width; i++) {
                        displayStrs[y].Add(new Pixel(' '));
                    }
                }

                if (displayStrs[y].Count > display.Width) {
                    displayStrs[y].RemoveRange(display.Width, displayStrs[y].Count - display.Width);
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
                str += pixel.Character + "\x1b[0m";
            }
            str += "\n";
        }

        return str;
    }

    public void Render() {
        Console.SetCursorPosition(0, 0);
        Console.Write(GetDisplayStr());
    }

    public Renderer(params Display[] displays) {
        Displays.AddRange(displays);
    }

    Thread? renderThread;
    Action? renderKill;

    public int RenderFrequency { get; set; } = 30;
    public bool UseIdealRenderFrequency { get; set; } = true;
    public bool UseThreadedRender { get; set; } = true;

    public void StartRenderThread(bool overrideThread = false) {
        if (renderThread != null) {
            if (overrideThread)
                renderKill?.Invoke();
            else return;
        }

        DateTime pastRender = DateTime.Now;

        renderThread = new System.Threading.Thread(() => {
            bool isRendering = true;
            while (isRendering) {
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
                    isRendering = false;
                };
            }
        });
        renderThread.Start();
    }

    public void StopRenderThread() {
        renderKill?.Invoke();
    }
}