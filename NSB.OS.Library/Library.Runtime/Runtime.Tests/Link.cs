using System.Runtime.InteropServices;

namespace NSB.OS.Runtime.Tests;

public static class Link {
    // Import void test() from lib/libNSB.OS.Library.so
    [DllImport("lib/libNSB.OS.Library.so", EntryPoint = "TestLink")]
    public static extern void TestLink();
}
