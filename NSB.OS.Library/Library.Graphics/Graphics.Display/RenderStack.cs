using NSB.OS.Logic.Threads;
using System.Diagnostics;
using System.Threading;

namespace NSB.OS.Graphics.DisplayNS;

public class RendererConfig
{
    public int RenderFrequency { get; set; } = 30;
    public bool UseIdealRenderFrequency { get; set; } = true;
    public bool UseThreadedRender { get; set; } = true;
    public bool UseManagedThreadedRender { get; set; } = true;
}

public class RendererStack
{
    public List<Display> Displays { get; set; } = new List<Display>();
    public RendererConfig Config { get; set; } = new RendererConfig();
    public bool IsRendering { get; private set; } = false;

    public void AddDisplay(Display display)
    {
        Displays.Add(display);
    }

    public void RemoveDisplay(Display display)
    {
        Displays.Remove(display);
    }

    private Pixel[,] CreateAllocatedArray() => new Pixel[Console.WindowHeight, Console.WindowWidth];

    public Pixel[,] GetDisplayData()
    {
        Pixel[,] displayStr = CreateAllocatedArray();

        foreach (Display display in Displays)
        {
            PixelMap pixels = display.GetPixels();

            for (int y = 0; y < display.Height; y++)
            {
                for (int x = 0; x < display.Width; x++)
                {
                    int realY = y + display.ViewY;
                    int realX = x + display.ViewX;

                    if (realY < 0 || realX < 0 || realY >= displayStr.GetLength(0) || realX >= displayStr.GetLength(1)) continue;

                    displayStr[realY, realX] = pixels.GetPixel(x, y);
                }
            }
        }

        return displayStr;
    }

    private Pixel[,] oldBuffer = new Pixel[0, 0];

    public void Render(bool refresh = false)
    {
        Pixel[,] buffer = GetDisplayData();
        // Get the differences and only print what we have to
        Console.Write("\x1b[0;0H");
        var drawAll = buffer.Length != oldBuffer.Length || refresh;

        for (int y = 0; y < buffer.GetLength(0); y++)
        {
            for (int x = 0; x < buffer.GetLength(1); x++)
            {
                Pixel nullPixel = new Pixel();
                if (buffer[y, x] == nullPixel) continue;
                string toString = buffer[y, x].ToString();
                if (drawAll)
                {
                    Console.Write(toString);
                    continue;
                }

                if (buffer[y, x] != oldBuffer[y, x])
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(toString);
                }
            }

            if (drawAll && y != buffer.GetLength(0) - 1) Console.Write("\n");
        }
        oldBuffer = buffer;
    }

    public RendererStack(params Display[] displays)
    {
        Displays.AddRange(displays);
    }

    private Thread? renderThread;
    private Action? renderKill;
    private DateTime fpsPast = DateTime.Now;
    private DateTime fpsNow = DateTime.Now;
    public int FPS = 0;

    public void StartRenderThread(bool overrideThread = false)
    {
        IsRendering = true;

        if (renderThread != null)
        {
            if (overrideThread)
                renderKill?.Invoke();
            else return;
        }

        DateTime pastRender = DateTime.Now;

        renderThread = new Thread(() =>
        {
            bool isLoopRendering = true;
            while (isLoopRendering)
            {
                fpsNow = DateTime.Now;

                if (this.Config.UseThreadedRender)
                {
                    if (this.Config.UseManagedThreadedRender)
                    {
                        ThreadManager.ThreadCall(() =>
                        {
                            this.Render();
                        }).Catch((e) =>
                        {
                            Console.WriteLine("Failed to render: " + e.ToString());
                            Environment.Exit(1);
                        });
                    }
                    else
                    {
                        new Thread(() =>
                        {
                            this.Render();
                        }).Start();
                    }
                }
                else
                {
                    this.Render();
                }

                if (this.Config.UseIdealRenderFrequency)
                {
                    double time = (DateTime.Now - pastRender).TotalMilliseconds;
                    if (((1000 / this.Config.RenderFrequency) - time) < 0) time = 0;
                    Thread.Sleep((int)(1000 / this.Config.RenderFrequency - time));
                    pastRender = DateTime.Now;
                }
                else Thread.Sleep(1000 / this.Config.RenderFrequency);

                renderKill = () =>
                {
                    isLoopRendering = false;
                };

                fpsPast = DateTime.Now;
                FPS = (int)(1000 / (fpsPast - fpsNow).TotalMilliseconds);
            }
        });
        renderThread.Start();
    }

    public void StopRenderThread()
    {
        renderKill?.Invoke();
        renderThread = null;
        IsRendering = false;
    }
}
