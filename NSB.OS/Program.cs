using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;
using NSB.OS.Runtime.ProgramsNS;

namespace NSB.OS;

public static class OS {
    public static void Main(String[] args) {
        int width = 80;
        int height = Console.WindowHeight;
        Console.CursorVisible = false;
        Console.Clear();

        Display home = new Display(new Vector2i(0, 0), new Vector2i(width, height));
        RectangleElement bg = new RectangleElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(bg);
        CenteredTextElement t = new CenteredTextElement(0, 1, "NSB_OS", width, null, null);
        home.AddElement(t);
        TextBarElement r = new TextBarElement(4, 2, width - 5, 2, '─', new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(r);
        OutlineElement o = new OutlineElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(0, 100, 255));
        home.AddElement(o);

        List<ProgramExecutable> apps = Programs.ListApps();
        List<CenteredTextElement> appTexts = new List<CenteredTextElement>();
        int pos = 3;
        for (int i = 0; i < apps.Count; i++) {
            if (apps[i].name == "NSB.OS" || apps[i].name == "NSB.OS.Library") continue;
            CenteredTextElement appText = new CenteredTextElement(0, pos++, apps[i].name, width, null, null);
            appTexts.Add(appText);
            home.AddElement(appText);
        }

        CharElement cursor = new CharElement(0, 0, 'X', null, new RGB(50, 100, 255));
        home.AddElement(cursor);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(home);
        renderer.Render();

        int cursorIndex = 0;
        char[] fadeChars = new char[] {
            '█',
            '▓',
            '▒',
            '░',
            '*',
            'X',
            'O',
            '>',
            '<',
            '^',
            '.'
        };

        while (true) {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.W) cursor.Y--;
            if (key.Key == ConsoleKey.S) cursor.Y++;
            if (key.Key == ConsoleKey.A) cursor.X--;
            if (key.Key == ConsoleKey.D) cursor.X++;

            if (key.Key == ConsoleKey.E) {
                cursorIndex++;
                if (cursorIndex >= fadeChars.Length) cursorIndex = 0;
                cursor.Character = fadeChars[cursorIndex];
            }

            if (key.Key == ConsoleKey.Spacebar) {
                int cursorRelativeY = cursor.Y - 3;
                if (cursorRelativeY >= 0 && cursorRelativeY < apps.Count) {
                    int textLength = apps[cursorRelativeY].name.Length;
                    int textStart = (width / 2) - (textLength / 2);
                    if (textLength % 2 != 0) textStart--;
                    int textEnd = textStart + textLength;
                    textEnd--;
                    
                    if (cursor.X >= textStart && cursor.X <= textEnd) {
                        Console.Clear();
                        Console.CursorVisible = true;
                        Programs.RunProgramExecutable(apps[cursorRelativeY]);

                        Console.Write("\n\nProcess exited. Press any key to continue... ");
                        Console.ReadKey(true);
                        Console.CursorVisible = false;
                        renderer.Render(true);
                    }
                }
            }

            // t.Text = $"X: {cursor.X}, Y: {cursor.Y}";

            renderer.Render();
        }
    }
}
