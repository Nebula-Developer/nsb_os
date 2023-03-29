
namespace NSB.OS.FileSystem;

public static class FSInit {
    private class FSTemplate {
        public List<string> DirectoryTree = new List<string>();
        public List<Tuple<string, string?>> FileData = new List<Tuple<string, string?>>();

        public void SplashTemplate(Drive root) {
            foreach (string dir in DirectoryTree)
                root.CreateDir(dir);

            foreach (Tuple<string, string?> file in FileData)
                if (file.Item2 == null) root.Create(file.Item1);
                else root.TryWrite(file.Item1, file.Item2);
        }
    }

    private static FSTemplate rootTemplate = new() {
        DirectoryTree = new()
{ "System", "System/Private", "System/Data", "System/Test", "Temp", "Users", "Users/Shared", "Config", "Config/Themes", "Config/Themes/Default", "Config/Preferences", "Config/Rules", "Config/LoadScripts" }, FileData = new()
{ new("System/Private/lock", "0"), new("System/Test/firstload", "1"), new("Temp/firstload", "1"), new("Config/Themes/Default/Theme.json", "{}"), new("Config/Preferences/Preferences.json", "{\"os\": {\"theme\": \"Default\"}}"), new("Config/Rules/Rules.json", "{}"), new("Config/LoadScripts/script1.nsc", "print(\"Hello World!\");") }
    };

    public static void Initialize(Drive drive) => rootTemplate.SplashTemplate(drive);

    public static bool CheckInitialized(Drive drive, bool deep = false) {
        if (!deep) {
            if (drive.Exists("System/Private/lock") && drive.GetText("System/Private/lock") == "1") return true;
            return true;
        }

        foreach (string dir in rootTemplate.DirectoryTree)
            if (!drive.Exists(dir) || !drive.IsDir(dir)) return false;

        foreach (Tuple<string, string?> file in rootTemplate.FileData)
            if (!drive.Exists(file.Item1) || !drive.IsFile(file.Item1)) return false;

        return true;
    }
}
