using System;
using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using NSB.OS.Runtime.Tests;
using NSB.OS.Logic.Threads;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Console.WriteLine("This project is not yet functional. Please come back later.");
        Link.TestLink();

        ThreadManager.ThreadCall(() => {
            // Create an error:
            int a = 0;
            int b = 1 / a;
        }).Then(() => {
            Console.WriteLine("Then");
        }).Catch((e) => {
            Console.WriteLine("Catch: " + e.Message);
        });

        Console.WriteLine("After Thread");
        Console.ReadLine();

        Display t = new Display(new Vector2i(0, 2), new Vector2i(20, 20).ToCharSquare());
        Display t2 = new Display(new Vector2i(0, 2), new Vector2i(17, 10).ToCharSquare());
        t.SetPixel(new Vector2i(0, 0), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
        t.SetPixel(new Vector2i(1, 0), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
        t2.SetPixel(new Vector2i(0, 0), new Pixel(' ', new RGB(255, 100, 200), new RGB(0, 255, 0)));
        t2.SetPixel(new Vector2i(1, 0), new Pixel(' ', new RGB(255, 100, 200), new RGB(0, 255, 0)));
        Renderer r = new Renderer(t, t2);
        r.RenderFrequency = 15;

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
            // Console.SetCursorPosition(0, 0);
            double time = Math.Round((DateTime.Now - dt).TotalMilliseconds, 2);
            times.Add(time);
            if (times.Count > 50) times.RemoveAt(0);
            dt = DateTime.Now;
            double average = getAverage();
            double fps = Math.Round(1000 / average, 2);
            double ideal = 1000 / r.RenderFrequency;
            double max = getMax(), min = getMin();

            // Console.SetCursorPosition(0, 0);
            // Console.WriteLine(new string(' ', Console.WindowWidth));
            // Console.SetCursorPosition(0, 0);
            // Console.WriteLine($"Average: {average}ms \t\t Ideal: {ideal}ms \t\t FPS: {fps} \t\t Max: {max}ms \t\t Min: {min}ms \t\t Render Frequency: {r.RenderFrequency}");

            // Console.SetCursorPosition(0, 1);
            // Console.WriteLine(new string(' ', Console.WindowWidth));
            // Console.SetCursorPosition(0, 1);
            // bool utr = r.UseThreadedRender;
            // bool uir = r.UseIdealRenderFrequency;
            // string yes = "[X]", no = "[ ]";
            // Console.WriteLine($"Use Threaded Render: {(utr ? yes : no)} \t\t Use Ideal Render Frequency: {(uir ? yes : no)}");

            t2.SetString(new Vector2i(0, 1), "Render Frequency: " + r.RenderFrequency);
            t2.SetString(new Vector2i(0, 2), "FPS: " + fps);
            t2.SetString(new Vector2i(0, 3), "Average: " + average);
            t2.SetString(new Vector2i(0, 4), "Ideal: " + ideal);
            t2.SetString(new Vector2i(0, 5), "Max: " + max);
            t2.SetString(new Vector2i(0, 6), "Min: " + min);

            t2.SetString(new Vector2i(0, 8), "Use Threaded Render: " + (r.UseThreadedRender ? "Yes" : "No "));
            t2.SetString(new Vector2i(0, 9), "Use Ideal Render Frequency: " + (r.UseIdealRenderFrequency ? "Yes" : "No "));
            
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
                    t.SetPixel(new Vector2i(oldX + 1, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    t.SetPixel(new Vector2i(x + 1, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.S) {
                    y++;
                    y = y > t.Height - 1 ? t.Height - 1 : y;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(oldX + 1, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    t.SetPixel(new Vector2i(x + 1, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.A) {
                    x-=2;
                    x = x < 0 ? 0 : x;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(oldX + 1, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    t.SetPixel(new Vector2i(x + 1, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    oldX = x;
                    oldY = y;
                } else if (key.Key == ConsoleKey.D) {
                    x+=2;
                    x = x > t.Width - 2 ? t.Width - 2 : x;
                    t.SetPixel(new Vector2i(oldX, oldY), new Pixel());
                    t.SetPixel(new Vector2i(oldX + 1, oldY), new Pixel());
                    t.SetPixel(new Vector2i(x, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
                    t.SetPixel(new Vector2i(x + 1, y), new Pixel(' ', new RGB(255, 0, 0), new RGB(0, 255, 0)));
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