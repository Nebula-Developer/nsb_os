#nullable disable

namespace NSB.OS.FileSystem;

public static class SystemDrives {
    public static Drive BootDrive = null;
    public static List<Drive> Drives = new List<Drive>();

    public static object IO { get; internal set; }

    public static void Init() {
        BootDrive = new Drive("Root");
    }
}
