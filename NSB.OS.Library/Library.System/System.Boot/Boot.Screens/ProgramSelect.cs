using NSB.OS.FileSystem;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.ProgramsNS;
using NSB.OS.SystemNS.InputNS;

namespace NSB.OS.SystemNS.BootNS.BootScreens;

public class ProgramSelect : Display {
    private RectangleElement Background;
    private OutlineElement Outline;
    private TextElement Title;
    public CursorElement Cursor;
    public List<ProgramExecutable> ProgramList = new List<ProgramExecutable>();
    public List<TextElement> ProgramElements = new List<TextElement>();
    private RendererStack Renderer;

    private static string[] oops = new string[] {
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

    public void UpdateListing() {
        foreach (TextElement element in ProgramElements) this.RemoveElement(element);
        ProgramElements.Clear();

        int i = 0;
        foreach (ProgramExecutable program in ProgramList) {
            TextElement element = new TextElement(0, 2 + i, program.name, TextConfig.Centered, null, new RGB(255, 255, 255));
            ProgramElements.Add(element);
            this.AddElement(element);
            i++;
        }
    }

    public ProgramSelect(Vector2i position, Vector2i size, RendererStack renderer) : base(position, size) {
        Background = new RectangleElement(0, 0, this.Width, this.Height, new RGB(0, 0, 0), new RGB(0, 0, 0));
        Outline = new OutlineElement(0, 0, this.Width, this.Height, null, new RGB(255, 255, 255));
        Title = new TextElement(0, 0, "[ Program Select ]", TextConfig.Centered, null, new RGB(255, 255, 255));
        Cursor = new CursorElement(1, 1, null, new RGB(255, 255, 255));

        this.AddElement(Background);
        this.AddElement(Outline);
        this.AddElement(Title);

        foreach (ProgramExecutable program in Programs.ListApps(SystemDrives.BootDrive)) {
            ProgramList.Add(program);
        }

        UpdateListing();

        this.AddElement(Cursor);

        Input.AddKeyAction(OnKeyPress);
        this.Renderer = renderer;
    }

    public void OnKeyPress() {
        if (!Input.KeyAvailable) return;
        Input.KeyAvailable = false;

        ConsoleKeyInfo key = Input.Key;
        bool fullRefresh = false;

        if (key.Key == ConsoleKey.W) {
            if (Cursor.Y > 1) Cursor.Y--;
        } else if (key.Key == ConsoleKey.S) {
            if (Cursor.Y < this.Height - 2) Cursor.Y++;
        } else if (key.Key == ConsoleKey.A) {
            if (Cursor.X > 1) Cursor.X--;
        } else if (key.Key == ConsoleKey.D) {
            if (Cursor.X < this.Width - 2) Cursor.X++;
        } else if ((key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar) && Cursor.Y > 1 && Cursor.Y - 2 < ProgramList.Count) {
            int textWidth = ProgramList[Cursor.Y - 2].name.Length;
            int posStart = (this.Width / 2) - (textWidth / 2);
            int posEnd = (this.Width / 2) + (textWidth / 2);
            posEnd -= textWidth % 2 == 0 ? 1 : 0;

            if (Cursor.X >= posStart && Cursor.X <= posEnd) {
                ProgramExecutable program = ProgramList[Cursor.Y - 2];
                string oopsStr = oops[new Random().Next(0, oops.Length - 1)];
                Console.Write("\x1b[?1049h");
                Console.Clear();

                ProgramReturn programReturn = new ProgramReturn(1, null);
                Exception? exp = null;

                try {
                    programReturn = Programs.RunProgramExecutable(program);
                } catch (Exception e) {
                    exp = e;
                }

                if (programReturn.exitCode == 1) {
                    exp = exp ?? programReturn.exception ?? new Exception("Unknown error");
                    Console.WriteLine("\n" + oopsStr);
                    Console.WriteLine("Process crashed (ext:1), more details below:");
                    Console.WriteLine(exp);
                    Console.WriteLine("Plain: " + exp.Message);
                    Console.WriteLine("Process: " + exp.Source);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                } else {
                    Console.WriteLine("\nProcess exited (ext:" + programReturn.exitCode + ") successfully.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                Console.Write("\x1b[?1049l");
                fullRefresh = true;
            }
        } else if (key.Key == ConsoleKey.Tab) {
            if (Cursor.Y > 1 && Cursor.Y - 2 < ProgramList.Count) {
                int curIndex = Cursor.Y - 1 > ProgramList.Count - 1 ? 0 : Cursor.Y - 1;
                ProgramExecutable program = ProgramList[curIndex % ProgramList.Count];
                int textWidth = program.name.Length;
                int posStart = (this.Width / 2) - (textWidth / 2);
                Cursor.X = posStart;
                Cursor.Y = curIndex + 2;
            } else {
                ProgramExecutable program = ProgramList[0];
                int textWidth = program.name.Length;
                int posStart = (this.Width / 2) - (textWidth / 2);
                Cursor.X = posStart;
                Cursor.Y = 2;
            }
        }

        Renderer.Render(fullRefresh);
    }
}