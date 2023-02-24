using System.Runtime.InteropServices;

namespace NSB.OS.Graphics.DisplayNS;

public class Display {
    // Import void test() from lib/libNSB.OS.Library.so
    [DllImport("lib/libNSB.OS.Library.so", EntryPoint = "TestLink")]
    public static extern void TestLink();
}