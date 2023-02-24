using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Graphics.DisplayNS;

public class Display {
    public int Width { get; set; }
    public int Height { get; set; }

    public int ViewX { get; set; }
    public int ViewY { get; set; }

    private List<List<Pixel>> Pixels = new List<List<Pixel>>();
    public int RenderFrequency { get; set; } = 30;

    public Display(Vector2i position, Vector2i size) {
        ViewX = position.X;
        ViewY = position.Y;
        Width = size.X;
        Height = size.Y;
        AllocatePixels();
    }

    private void AllocatePixels() {
        for (int y = 0; y < Height; y++) {
            if (Pixels.Count <= y) Pixels.Add(new List<Pixel>());
            for (int x = 0; x < Width; x++) {
                if (Pixels[y].Count <= x) Pixels[y].Add(new Pixel(' '));
            }
        }
    }

    public void RenderBuffer() {
        for (int y = 0; y < Height; y++) {
            Console.SetCursorPosition(ViewX, ViewY + y);
            for (int x = 0; x < Width; x++) {
                if (Pixels.Count <= y || Pixels[y].Count <= x) {
                    AllocatePixels();
                }

                if (Pixels[y][x].BG != null) {
                    Console.Write(Pixels[y][x].BG?.ToBGESC());
                }
                if (Pixels[y][x].FG != null) {
                    Console.Write(Pixels[y][x].FG?.ToFGESC());
                }
                Console.Write(Pixels[y][x].Character + "\x1b[0m");
            }
        }
    }

    Thread renderThread;
    Action renderKill;
    public Action Update { get; set; }

    public void StartRenderThread(bool overrideThread = false) {
        if (renderThread != null) {
            if (overrideThread)
                renderKill?.Invoke();
            else return;
        }

        renderThread = new System.Threading.Thread(() => {
            bool isRendering = true;
            while (isRendering) {
                Update();
                new System.Threading.Thread(() => {
                    RenderBuffer();
                }).Start();
                System.Threading.Thread.Sleep(1000 / RenderFrequency);
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
