using System;
using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using NSB.OS.Runtime.Tests;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Console.WriteLine("This project is not yet functional. Please come back later.");
        Link.TestLink();
        Display t = new Display(new Vector2i(0, 2), new Vector2i(5, 5));
        Display t2 = new Display(new Vector2i(0, 2), new Vector2i(5, 5));
        t.SetPixel(new Vector2i(0, 0).ToCharSquare(), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
        t2.SetPixel(new Vector2i(0, 0).ToCharSquare(), new Pixel('X', new RGB(255, 100, 200), new RGB(0, 255, 0)));
        Renderer r = new Renderer(t, t2);

        DateTime dt = DateTime.Now;
        List<double> times = new List<double>();
        double getAverage() {
            double total = 0;
            foreach (double d in times) {
                total += d;
            }
            double av = Math.Round(total / times.Count, 2);
            return av;
        }
        double getMax() {
            double max = 0;
            foreach (double d in times) {
                if (d > max) max = d;
            }
            return max;
        }
        double getMin() {
            double min = double.MaxValue;
            foreach (double d in times) {
                if (d < min && min != 0) min = d;
            }
            return min;
        }

        Console.CursorVisible = false;
        Console.Clear();

        int x = 0, y = 0;
        int oldX = 0, oldY = 0;

        t.Update = () => {
            Console.SetCursorPosition(0, 0);
            double time = Math.Round((DateTime.Now - dt).TotalMilliseconds, 2);
            times.Add(time);
            if (times.Count > 50) times.RemoveAt(0);
            dt = DateTime.Now;
            double average = getAverage();
            double fps = Math.Round(1000 / average, 2);
            double ideal = 1000 / r.RenderFrequency;
            double max = getMax(), min = getMin();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Average: {average}ms \t\t Ideal: {ideal}ms \t\t FPS: {fps} \t\t Max: {max}ms \t\t Min: {min}ms \t\t Render Frequency: {r.RenderFrequency}");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 1);
            bool utr = r.UseThreadedRender;
            bool uir = r.UseIdealRenderFrequency;
            string yes = "[X]", no = "[ ]";
            Console.WriteLine($"Use Threaded Render: {(utr ? yes : no)} \t\t Use Ideal Render Frequency: {(uir ? yes : no)}");
            
            if (Console.KeyAvailable) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) {
                    Console.CursorVisible = true;
                }
                if (key.Key == ConsoleKey.K) {
                    r.RenderFrequency += 5;
                    times.Clear();
                }
                if (key.Key == ConsoleKey.J) {
                    r.UseIdealRenderFrequency = !r.UseIdealRenderFrequency;
                    times.Clear();
                }
                if (key.Key == ConsoleKey.H) {
                    r.RenderFrequency -= 5;
                    r.RenderFrequency = r.RenderFrequency <= 0 ? 1 : r.RenderFrequency;
                    times.Clear();
                }

                if (key.Key == ConsoleKey.W) {
                    y--;
                    y = y < 0 ? 0 : y;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.S) {
                    y++;
                    y = y > t.Height - 1 ? t.Height - 1 : y;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.A) {
                    x--;
                    x = x < 0 ? 0 : x;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.D) {
                    x++;
                    x = x > t.Width - 1 ? t.Width - 1 : x;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                }
                else if (key.Key == ConsoleKey.G) {
                    r.UseThreadedRender = !r.UseThreadedRender;
                    times.Clear();
                }
            }
        };

        // t.StartRenderThread();
        r.StartRenderThread();
    }
}