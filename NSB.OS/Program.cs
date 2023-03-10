using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Runtime.Tests;
using NSB.OS.FileSystem;
using NSB.OS.SystemNS.BootNS;
using NSB.OS.SystemNS.BootNS.BootScreens;
using NSB.OS.SystemNS.InputNS;

namespace NSB.OS;

public static class OS
{
    private static Drive root => SystemDrives.BootDrive;

    public static void Main(String[] args)
    {
        Boot.Init();

        if (args.Contains("--test-link") || args.Contains("-t"))
        {
            Link.TestLink();
            return;
        }

        RendererStack renderer = new RendererStack();
        ProgramSelect programSelect = new ProgramSelect(new Vector2i(0, 0), new Vector2i(80, Math.Min(Console.WindowHeight, 25)), renderer);
        renderer.AddDisplay(programSelect);
        renderer.Render();
    }
}
