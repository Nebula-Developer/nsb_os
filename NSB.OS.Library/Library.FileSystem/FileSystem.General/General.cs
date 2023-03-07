using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

using System.IO;

namespace NSB.OS.FileSystem;

public static partial class FS {
    public static NSFile Read(string path) {
        if (!File.Exists(path)) throw new FileNotFoundException($"File not found: {path}");
        return new NSFile(path);
    }

    public static NSFile? TryRead(string path) => File.Exists(path) ? new NSFile(path) : null;

    public static NSFile Write(string path) => new NSFile(path).Create();
    public static NSFile Write(string path, string text) => new NSFile(path).Write(text);
    public static NSFile Write(string path, string[] lines) => new NSFile(path).Write(lines);

    public static NSFile Append(string path, string text) => new NSFile(path).Append(text);
    public static NSFile Append(string path, string[] lines) => new NSFile(path).Append(lines);

    public static NSFile Delete(string path) => new NSFile(path).Delete();
    public static NSFile Create(string path) => new NSFile(path).Create();

    public static NSDir Mkdir(string path) => new NSDir(path).Create();
    public static NSDir Rmdir(string path) => new NSDir(path).Delete();
}

public class NSFile {
    public string Path { get; set; }
    public string[] Lines => File.ReadAllLines(Path);
    public string Text => File.ReadAllText(Path);

    public NSFile(string path) {
        Path = path;
    }

    public NSFile Write(string text) {
        File.WriteAllText(Path, text);
        return this;
    }

    public NSFile Write(string[] lines) {
        File.WriteAllLines(Path, lines);
        return this;
    }

    public NSFile Append(string text) {
        File.AppendAllText(Path, text);
        return this;
    }

    public NSFile Append(string[] lines) {
        File.AppendAllLines(Path, lines);
        return this;
    }

    public NSFile Delete() {
        File.Delete(Path);
        return this;
    }

    public NSFile Create() {
        if (File.Exists(Path)) throw new FileLoadException($"File already exists: {Path}");
        File.Create(Path).Close();
        return this;
    }
}

public class NSDir {
    public string Path { get; set; }
    public string[] Files => Directory.GetFiles(Path);
    public string[] Directories => Directory.GetDirectories(Path);

    public NSDir(string path) {
        Path = path;
    }

    public NSDir Delete() {
        Directory.Delete(Path);
        return this;
    }

    public NSDir Create() {
        if (Directory.Exists(Path)) throw new DirectoryNotFoundException($"Directory already exists: {Path}");
        Directory.CreateDirectory(Path);
        return this;
    }
}
