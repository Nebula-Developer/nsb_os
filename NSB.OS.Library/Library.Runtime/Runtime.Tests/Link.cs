using System.Runtime.InteropServices;

namespace NSB.OS.Runtime.Tests;

public static class Link {
    // Import void test() from lib/libNSB.OS.Library.so
    [DllImport("lib/libNSB.OS.Library.so", EntryPoint = "TestLink")]
    public static extern void TestLink();

    // Import InitLinkLibrary()
    [DllImport("lib/libNSB.OS.Library.so", EntryPoint = "InitLinkLibrary")]
    public static extern void InitLinkLibrary();

    // Import MoveCursor(int x, int y)
    [DllImport("lib/libNSB.OS.Library.so", EntryPoint = "MoveCursor")]
    public static extern void MoveCursor(int x, int y);
}
