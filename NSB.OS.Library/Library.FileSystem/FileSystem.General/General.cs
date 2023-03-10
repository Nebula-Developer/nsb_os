using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

using System.IO;

namespace NSB.OS.FileSystem;

public class NSFile
{
    public string Path { get; private set; }
    public static string GetPath(string path) => path;

    public string Name => System.IO.Path.GetFileName(Path);
    public string Extension => System.IO.Path.GetExtension(Path);

    public string Text => System.IO.File.ReadAllText(Path);
    public string[] Lines => System.IO.File.ReadAllLines(Path);
    public int Bytes => System.IO.File.ReadAllBytes(Path).Length;
    public System.IO.FileInfo Info => new(Path);
    public bool Exists() => System.IO.File.Exists(Path);

    public bool Create()
    {
        if (Exists()) return false;
        System.IO.File.Create(Path).Close();
        return true;
    }

    public NSFile(string path) => Path = GetPath(path);
}

public class NSDir
{
    public string Path { get; private set; }
    public static string GetPath(string path) => path;

    public string Name => System.IO.Path.GetFileName(Path);
    public string[] Files => System.IO.Directory.GetFiles(Path);
    public string[] Directories => System.IO.Directory.GetDirectories(Path);

    public NSFile[] NSFiles => Files.Select(file => new NSFile(file)).ToArray();
    public NSDir[] NSDirs => Directories.Select(dir => new NSDir(dir)).ToArray();
    public bool Exists() => System.IO.Directory.Exists(Path);

    public bool Create()
    {
        if (Exists()) return false;
        System.IO.Directory.CreateDirectory(Path);
        return true;
    }

    public NSDir(string path) => Path = GetPath(path);
}
