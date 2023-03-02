using NSB.OS.Graphics.Mathematics;
using NSB.OS.Logic.Threads;

namespace NSB.OS.Graphics.CursorNS;

public class Cursor {
    public int X { get; set; }
    public int Y { get; set; }
    
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }

    public Cursor(int x, int y, RGB? bg = null, RGB? fg = null) {
        this.X = x;
        this.Y = y;
        BG = bg;
        FG = fg;
    }

    public Cursor(Vector2i pos, RGB? bg = null, RGB? fg = null) {
        this.X = pos.X;
        this.Y = pos.Y;
        BG = bg;
        FG = fg;
    }

    public void DrawThread() {
        while (true) {
            Console.SetCursorPosition(X, Y);
            char emptyChar = '░';

            Console.Write(BG?.ToBGESC() + FG?.ToFGESC() + emptyChar);
            Thread.Sleep(100);

            if (Console.KeyAvailable) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        Y--;
                        break;
                    case ConsoleKey.DownArrow:
                        Y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        X--;
                        break;
                    case ConsoleKey.RightArrow:
                        X++;
                        break;
                }
            }
        }
    }

    public void Draw() {
        ThreadCall c = ThreadManager.ThreadCall(DrawThread).Then(() => {
            Console.SetCursorPosition(X, Y);
            Console.Write(BG?.ToBGESC() + FG?.ToFGESC() + '█');
        });
    }
}
