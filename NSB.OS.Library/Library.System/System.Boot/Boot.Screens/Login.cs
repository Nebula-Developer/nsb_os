using NSB.OS.FileSystem;
using NSB.OS.Graphics;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Logic.AccountsNS;
using NSB.OS.Runtime.ProgramsNS;
using NSB.OS.SystemNS.InputNS;

namespace NSB.OS.SystemNS.BootNS.BootScreens;

public class Login : Display {
    private RectangleElement Background;
    private OutlineElement Outline;
    private TextElement Title;
    public CursorElement Cursor;
    public List<Account> AccountList = new List<Account>();
    public List<TextElement> AccountElements = new List<TextElement>();
    private RendererStack Renderer;

    public void UpdateListing() {
        foreach (TextElement element in AccountElements) this.RemoveElement(element);
        AccountElements.Clear();

        int i = 0;
        foreach (Account program in AccountList) {
            TextElement element = new(0, 2 + i, program.Username, TextConfig.Centered, null, new RGB(255, 255, 255));
            AccountElements.Add(element);
            this.AddElement(element);
            i++;
        }
    }

    public Login(Vector2i position, Vector2i size, RendererStack renderer) : base(position, size) {
        Background = new RectangleElement(0, 0, this.Width, this.Height, new RGB(0, 0, 0), new RGB(0, 0, 0));
        Outline = new OutlineElement(0, 0, this.Width, this.Height, null, new RGB(255, 255, 255));
        Title = new TextElement(0, 0, "[ Login ]", TextConfig.Centered, null, new RGB(255, 255, 255));
        Cursor = new CursorElement(1, 1, null, new RGB(255, 255, 255));

        this.AddElement(Background);
        this.AddElement(Outline);
        this.AddElement(Title);

        foreach (Account program in Accounts.GetAccounts ?? new List<Account>()) {
            AccountList.Add(program);
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
        } else if ((key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar) && Cursor.Y > 1 && Cursor.Y - 2 < AccountList.Count) {
            int textWidth = AccountList[Cursor.Y - 2].Username.Length;
            int posStart = (this.Width / 2) - (textWidth / 2);
            int posEnd = (this.Width / 2) + (textWidth / 2);
            posEnd -= textWidth % 2 == 0 ? 2 : 0;
            posStart--;

            if (Cursor.X >= posStart && Cursor.X <= posEnd) {
                Account c = AccountList[Cursor.Y - 2];
                AccountElements.ForEach((e) => { e.Visible = false; });
                Title.Text = "[ Login to " + c.Username + " ]";
                // fullRefresh = true;
            }
        } else if (key.Key == ConsoleKey.Tab) {
            if (Cursor.Y > 1 && Cursor.Y - 2 < AccountList.Count) {
                int curIndex = Cursor.Y - 1 > AccountList.Count - 1 ? 0 : Cursor.Y - 1;
                Account program = AccountList[curIndex % AccountList.Count];
                int textWidth = program.Username.Length;
                int posStart = (this.Width / 2) - (textWidth / 2);
                Cursor.X = posStart - 1;
                Cursor.Y = curIndex + 2;
            } else {
                Account program = AccountList[0];
                int textWidth = program.Username.Length;
                int posStart = (this.Width / 2) - (textWidth / 2);
                Cursor.X = posStart - 1;
                Cursor.Y = 2;
            }
        }

        Renderer.Render(fullRefresh);
    }
}