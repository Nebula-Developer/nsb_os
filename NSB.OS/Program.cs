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
        RectangleElement bg = new RectangleElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(255, 160, 255));
        home.AddElement(bg);
        CenteredTextElement t = new CenteredTextElement(0, 1, "NSB_OS", width, null, null);
        home.AddElement(t);
        TextBarElement r = new TextBarElement(4, 2, width - 5, 2, '─', new RGB(0, 0, 0), new RGB(120, 0, 255));
        home.AddElement(r);
        OutlineElement o = new OutlineElement(0, 0, width, height, new RGB(0, 0, 0), new RGB(120, 0, 255));
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
                        // Switch to scrollback
                        Console.Write("\x1b[?1049h");
                        Console.Clear();
                        Console.CursorVisible = true;
                        ProgramReturn? programReturn = null;
                        try {
                            programReturn = Programs.RunProgramExecutable(apps[cursorRelativeY]);
                        } catch {
                            appTexts[cursorRelativeY].FG = new RGB(255, 0, 0);
                        }

                        if (programReturn == null) {
                            Console.Write("\n\nProcess failed to execute (ext:2). Press any key to continue...");
                        } else if (programReturn.exitCode == 0) {
                            Console.Write("\n\nProcess exited (ext:0). Press any key to continue...");
                        } else if (programReturn.exitCode == 1) {
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

                            Console.WriteLine("\n\n" + oops[new Random().Next(oops.Length)]);
                            Console.WriteLine("Process crashed (ext:1). More details below.");
                            Console.WriteLine(programReturn.exception);
                            Console.WriteLine("Plain error: " + programReturn.exception?.Message);
                            Console.WriteLine("Error type: " + programReturn.exception?.GetType().ToString());
                            Console.WriteLine("Press any key to continue...");
                        } else {
                            Console.Write("\n\nProcess exited (ext:{0}). Press any key to continue...", programReturn.exitCode);
                        }

                        Console.ReadKey(true);
                        Console.CursorVisible = false;
                        Console.Write("\x1b[?1049l");
                        renderer.Render(true);
                    }
                }
            }

            if (key.Key == ConsoleKey.Q) {
                pos = 3;
                apps = Programs.ListApps();
                for (int i = 0; i < Math.Max(apps.Count, appTexts.Count); i++) {
                    if (i >= apps.Count) {
                        home.RemoveElement(appTexts[i]);
                        appTexts = appTexts.Take(i).ToList();
                        continue;
                    }

                    if (apps[i].name == "NSB.OS" || apps[i].name == "NSB.OS.Library") continue;
                    if (i >= appTexts.Count) {
                        CenteredTextElement appText = new CenteredTextElement(0, pos++, apps[i].name, width, null, null);
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
