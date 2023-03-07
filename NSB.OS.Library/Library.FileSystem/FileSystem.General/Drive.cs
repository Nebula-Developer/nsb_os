
namespace NSB.OS.FileSystem;

public class Drive {
    public string Label { get; set; }
    public string Path => FSPath.Combine(FSPath.Root, Label);
    public string[] Files => System.IO.Directory.GetFiles(Path);

    public string GetFilePath(string[] paths) => FSPath.Combine(Path, FSPath.Combine(paths));
    public string GetFilePath(string path) => FSPath.Combine(Path, path);
    
    public NSFile Read(string path) => FS.Read(GetFilePath(path));
    public NSFile? TryRead(string path) => FS.TryRead(GetFilePath(path));

    public NSFile Write(string path, string text) => FS.Write(GetFilePath(path), text);
    public NSFile Write(string path, string[] lines) => FS.Write(GetFilePath(path), lines);

    public NSFile Append(string path, string text) => FS.Append(GetFilePath(path), text);
    public NSFile Append(string path, string[] lines) => FS.Append(GetFilePath(path), lines);

    public NSFile Delete(string path) => FS.Delete(GetFilePath(path));
    public NSFile Create(string path) => FS.Create(GetFilePath(path));

    public NSDir Mkdir(string path) => FS.Mkdir(GetFilePath(path));
    public NSDir Rmdir(string path) => FS.Rmdir(GetFilePath(path));

    public Drive(string label) {
        Label = label;
    }
}