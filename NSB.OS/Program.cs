using System;
using System.Collections.Generic;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        Console.WriteLine("This project is not yet functional. Please come back later.");
        NSB.OS.Runtime.Tests.Link.TestLink();
        Display t = new Display(new Vector2i(5, 5).ToCharSquare(), new Vector2i(10, 10).ToCharSquare());
        t.SetPixel(new Vector2i(4, 4).ToCharSquare(), new Pixel('X', new RGB(255, 0, 0), new RGB(0, 255, 0)));
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

        t.Update = () => {
            Console.SetCursorPosition(0, 0);
            times.Add(Math.Round((DateTime.Now - dt).TotalMilliseconds, 2));
            dt = DateTime.Now;
            double average = getAverage();
            double fps = Math.Round(1000 / average, 2);
            double ideal = 1000 / t.RenderFrequency;
            double max = getMax(), min = getMin();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Average: {average}ms \t\t Ideal: {ideal}ms \t\t FPS: {fps} \t\t Max: {max}ms \t\t Min: {min}ms \t\t Render Frequency: {t.RenderFrequency}");
            
            if (Console.KeyAvailable) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) {
                    t.StopRenderThread();
                    Console.CursorVisible = true;
                }
                if (key.Key == ConsoleKey.K) {
                    t.RenderFrequency += 10;
                    times.Clear();
                }
            }
        };
        t.StartRenderThread();
    }
}