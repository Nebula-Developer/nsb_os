
namespace NSB.OS.FileSystem;

public class Drive : FS
{
    public string Label { get; set; }
    public string Path => FSPath.Combine(FSPath.Root, Label);
    public override string GetPath(string path) => FSPath.Combine(Path, path.TrimStart('/'));

    new public DriveFile GetFile(string path) => new(this, GetPath(path));
    new public DriveDir GetDir(string path) => new(this, GetPath(path));

    public Drive(string label) : base() => Label = label;
}

public class FS
{
    public virtual string GetPath(string path) => path;

    public string GetFileName(string path) => System.IO.Path.GetFileName(GetPath(path));
    public string GetExtension(string path) => System.IO.Path.GetExtension(GetPath(path));

    public string GetText(string path) => System.IO.File.ReadAllText(GetPath(path));
    public string[] GetLines(string path) => System.IO.File.ReadAllLines(GetPath(path));
    public int GetBytes(string path) => System.IO.File.ReadAllBytes(GetPath(path)).Length;

    public System.IO.FileInfo GetInfo(string path) => new(GetPath(path));
    public NSFile GetFile(string path) => new(GetPath(path));
    public NSDir GetDir(string path) => new(GetPath(path));

    public bool Exists(string path) => System.IO.File.Exists(GetPath(path)) || System.IO.Directory.Exists(GetPath(path));
    public bool Create(string path)
    {
        if (Exists(path)) return false;
        System.IO.File.Create(GetPath(path)).Close();
        return true;
    }

    public bool DirExists(string path) => System.IO.Directory.Exists(GetPath(path));
    public bool FileExists(string path) => System.IO.File.Exists(GetPath(path));
    public bool CreateDir(string path)
    {
        if (DirExists(path)) return false;
        System.IO.Directory.CreateDirectory(GetPath(path));
        return true;
    }

    public void Write(string path, string text) => System.IO.File.WriteAllText(GetPath(path), text);
    public void Write(string path, string[] lines) => System.IO.File.WriteAllLines(GetPath(path), lines);
    public void Write(string path, byte[] bytes) => System.IO.File.WriteAllBytes(GetPath(path), bytes);

    public bool TryWrite(string path, string text)
    {
        if (Exists(path)) return false;
        Write(path, text);
        return true;
    }

    public bool TryWrite(string path, string[] lines)
    {
        if (Exists(path)) return false;
        Write(path, lines);
        return true;
    }

    public bool TryWrite(string path, byte[] bytes)
    {
        if (Exists(path)) return false;
        Write(path, bytes);
        return true;
    }

    public void Append(string path, string text) => System.IO.File.AppendAllText(GetPath(path), text);
    public void Append(string path, string[] lines) => System.IO.File.AppendAllLines(GetPath(path), lines);
    public void Append(string path, byte[] bytes) => System.IO.File.WriteAllBytes(GetPath(path), System.IO.File.ReadAllBytes(GetPath(path)).Concat(bytes).ToArray());

    public void Delete(string path) => System.IO.File.Delete(GetPath(path));
    public void DeleteDir(string path) => System.IO.Directory.Delete(GetPath(path));
    public void DeleteDir(string path, bool recursive) => System.IO.Directory.Delete(GetPath(path), recursive);

    public void Copy(string source, string destination) => System.IO.File.Copy(GetPath(source), GetPath(destination));

    public void CopyDir(string source, string destination)
    {
        if (!DirExists(source)) return;
        if (!DirExists(destination)) CreateDir(destination);
        foreach (string file in System.IO.Directory.GetFiles(source)) Copy(file, FSPath.Combine(destination, GetFileName(file)));
        foreach (string dir in System.IO.Directory.GetDirectories(source)) CopyDir(dir, FSPath.Combine(destination, GetFileName(dir)));
    }

    public void Move(string source, string destination) => System.IO.File.Move(GetPath(source), GetPath(destination));

    public bool IsDir(string path) => System.IO.Directory.Exists(GetPath(path));
    public bool IsFile(string path) => System.IO.File.Exists(GetPath(path));

    public FS() { }
}

public class DriveFile : NSFile
{
    public Drive Drive { get; private set; }
    new public string Path => FSPath.Combine(Drive.Path, base.Path);

    public DriveFile(Drive drive, string path) : base(path) => Drive = drive;
}

public class DriveDir : NSDir
{
    public Drive Drive { get; private set; }
    new public string Path => FSPath.Combine(Drive.Path, base.Path);

    public DriveDir(Drive drive, string path) : base(path) => Drive = drive;
}
