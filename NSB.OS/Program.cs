using System;
using System.Collections.Generic;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;
using NSB.OS.Runtime.ProgramsNS;
using NSB.OS.FileSystem;

namespace NSB.OS;

public static class OS
{
    public static void Main(String[] args)
    {
        NSB.OS.Runtime.SignalsNS.Signals.CancelAll();
        Drive root = new Drive("Root");
        if (!FSInit.CheckInitialized(root, true)) FSInit.Initialize(root);
        if (!root.Exists("/Users/Shared/Programs")) root.CreateDir("/Users/Shared/Programs");

        if (args.Contains("--test-link") || args.Contains("-t"))
        {
            Link.TestLink();
            return;
        }

        int width = 80;
        int height = Math.Min(Console.WindowHeight, 25);
        Console.CursorVisible = false;
        Console.Clear();

        Display home = new Display(new Vector2i(0, 0), new Vector2i(width, height));
        RectangleElement bg = new RectangleElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(160, 0, 255));
        home.AddElement(bg);

        home.AddElement(new TextBarElement(4, 3, width - 5, 3, '─', new RGB(0, 0, 0), new RGB(120, 0, 255)));
        home.AddElement(new TextElement(0, 2, "Program List", new TextConfig()
        {
            Alignment = TextAlignment.Center,
            Width = width,
            Height = 1,
            Orientation = TextOrientation.Horizontal
        }, null, new RGB(135, 0, 255)));
        OutlineElement o = new OutlineElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(120, 0, 255));
        home.AddElement(o);

        List<ProgramExecutable> apps = Programs.ListApps(root);
        List<TextElement> appTexts = new List<TextElement>();
        int pos = 4;
        for (int i = 0; i < apps.Count; i++)
        {
            if (apps[i].name == "NSB.OS" || apps[i].name == "NSB.OS.Library") continue;
            TextElement appText = new TextElement(0, pos++, apps[i].name, TextConfig.Centered, null, null);
            appTexts.Add(appText);
            home.AddElement(appText);
        }

        CursorElement cursor = new CursorElement(1, 1, null, new RGB(150, 50, 255));
        home.AddElement(cursor);

        TextElement nsbOSTitle = new TextElement(0, 0, "NSB│OS", new TextConfig()
        {
            Alignment = TextAlignment.Center,
            Width = 1,
            Height = height,
            Orientation = TextOrientation.Vertical
        }, null, new RGB(120, 0, 255));

        TextElement programTitle = new TextElement(0, 0, "[ Programs ]", new TextConfig()
        {
            Alignment = TextAlignment.Center,
            Width = width,
            Height = 1,
            Orientation = TextOrientation.Horizontal
        }, null, new RGB(120, 0, 255));

        home.AddElement(nsbOSTitle);
        home.AddElement(programTitle);

        RendererStack renderer = new RendererStack();
        renderer.AddDisplay(home);
        renderer.Render();

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

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.W) cursor.Y--;
            if (key.Key == ConsoleKey.S) cursor.Y++;
            if (key.Key == ConsoleKey.A) cursor.X--;
            if (key.Key == ConsoleKey.D) cursor.X++;

            cursor.X = Math.Clamp(cursor.X, 1, width - 2);
            cursor.Y = Math.Clamp(cursor.Y, 1, height - 2);

            int cursorRelativeY = cursor.Y - 4;

            if (key.Key == ConsoleKey.Tab && apps.Count > 0)
            {
                if (cursorRelativeY >= 0 && cursorRelativeY < apps.Count)
                {
                    int textLength = apps[cursorRelativeY].name.Length;
                    int textStart = (width / 2) - (textLength / 2);
                    if (textLength % 2 != 0) textStart--;
                    int textEnd = textStart + textLength;
                    textEnd--;

                    if (cursor.X >= textStart && cursor.X <= textEnd)
                    {
                        // Move cursor to next app
                        cursorRelativeY++;
                        if (cursorRelativeY >= apps.Count) cursorRelativeY = 0;
                        cursor.Y = cursorRelativeY + 4;
                        cursor.X = (width / 2) - (apps[cursorRelativeY].name.Length / 2);
                        if (apps[cursorRelativeY].name.Length % 2 != 0) cursor.X--;
                    }
                    else
                    {
                        // Move cursor to first app
                        cursorRelativeY = 0;
                        cursor.Y = cursorRelativeY + 4;
                        cursor.X = (width / 2) - (apps[0].name.Length / 2);
                        if (apps[0].name.Length % 2 != 0) cursor.X--;
                    }
                }
                else
                {
                    // Move cursor to first app
                    cursorRelativeY = 0;
                    cursor.Y = cursorRelativeY + 4;
                    cursor.X = (width / 2) - (apps[0].name.Length / 2);
                    if (apps[0].name.Length % 2 != 0) cursor.X--;
                }
            }

            for (int i = 0; i < appTexts.Count; i++)
            {
                appTexts[i].BG = null;
            }

            if (cursorRelativeY >= 0 && cursorRelativeY < apps.Count)
            {
                int textLength = apps[cursorRelativeY].name.Length;
                int textStart = (width / 2) - (textLength / 2);
                if (textLength % 2 != 0) textStart--;
                int textEnd = textStart + textLength;
                textEnd--;
                if (cursor.X >= textStart && cursor.X <= textEnd)
                {
                    appTexts[cursorRelativeY].BG = new RGB(30, 0, 60);
                }
                else
                {
                    appTexts[cursorRelativeY].BG = null;
                }
            }

            if (key.Key == ConsoleKey.Spacebar)
            {
                if (cursorRelativeY >= 0 && cursorRelativeY < apps.Count)
                {
                    int textLength = apps[cursorRelativeY].name.Length;
                    int textStart = (width / 2) - (textLength / 2);
                    if (textLength % 2 != 0) textStart--;
                    int textEnd = textStart + textLength;
                    textEnd--;

                    if (cursor.X >= textStart && cursor.X <= textEnd)
                    {
                        // Switch to scrollback
                        Console.Write("\x1b[?1049h");
                        Console.Clear();
                        Console.CursorVisible = true;
                        ProgramReturn? programReturn = null;

                        string[] oops = new string[] {
                            "Oops!",
                            "Well, that's embarrassing.",
                            "Oh no!",
                            "My bad, let me fix that.",
                            "Nope, that's not right.",
                            "Did I do that?",
                            "I spoke too soon.",
                            "My apologies.",
                            "Eek, that wasn't supposed to happen.",
                            "Looks like I made a mistake.",
                            "Uh-oh, let's try that again.",
                            "My mistake, sorry about that.",
                            "Well, that's not ideal.",
                            "That didn't go as planned."
                        };

                        try
                        {
                            programReturn = Programs.RunProgramExecutable(apps[cursorRelativeY]);
                        }
                        catch (Exception exception)
                        {
                            appTexts[cursorRelativeY].FG = new RGB(255, 0, 0);
                            Console.WriteLine("\n\n" + oops[new Random().Next(oops.Length)]);
                            Console.WriteLine("Process crashed (ext:1). More details below.");
                            Console.WriteLine(exception);
                            Console.WriteLine("Plain error: " + exception.Message);
                            Console.WriteLine("Error type: " + exception.GetType().ToString());
                            Console.WriteLine("Press any key to continue...");
                        }

                        if (programReturn == null)
                        {
                            Console.Write("\n\nProcess failed to execute (ext:2). Press any key to continue...");
                        }
                        else if (programReturn.exitCode == 0)
                        {
                            Console.Write("\n\nProcess exited (ext:0). Press any key to continue...");
                        }
                        else if (programReturn.exitCode == 1)
                        {
                            Console.WriteLine("\n\n" + oops[new Random().Next(oops.Length)]);
                            Console.WriteLine("Process crashed (ext:1). More details below.");
                            Console.WriteLine(programReturn.exception);
                            Console.WriteLine("Plain error: " + programReturn.exception?.Message);
                            Console.WriteLine("Error type: " + programReturn.exception?.GetType().ToString());
                            Console.WriteLine("Press any key to continue...");
                        }
                        else
                        {
                            Console.Write("\n\nProcess exited (ext:{0}). Press any key to continue...", programReturn.exitCode);
                        }

                        Console.ReadKey(true);
                        Console.CursorVisible = false;
                        Console.Write("\x1b[?1049l");
                        renderer.Render(true);
                    }
                }
            }

            if (key.Key == ConsoleKey.Q)
            {
                pos = 4;
                apps = Programs.ListApps(root);
                for (int i = 0; i < Math.Max(apps.Count, appTexts.Count); i++)
                {
                    if (i >= apps.Count)
                    {
                        home.RemoveElement(appTexts[i]);
                        appTexts = appTexts.Take(i).ToList();
                        continue;
                    }

                    if (apps[i].name == "NSB.OS" || apps[i].name == "NSB.OS.Library") continue;
                    if (i >= appTexts.Count)
                    {
                        TextElement appText = new TextElement(pos, pos++, apps[i].name, TextConfig.Right, null, null);
                        appTexts.Add(appText);
                    }

                    if (!home.Elements.Contains(appTexts[i])) home.AddElement(appTexts[i]);
                    appTexts[i].Y = pos++;
                    appTexts[i].Text = apps[i].name;
                    appTexts[i].FG = null;
                }
            }

            renderer.Render();
        }
    }
}
