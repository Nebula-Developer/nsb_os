using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;
using System.Text.Json;
using NSB.OS.FileSystem;

namespace NSB.OS.Logic.AccountsNS;

public class Database {
    public string Path { get; set; }
    public string RelativePath => PathFromDrive ? Drive != null ? Drive.GetPath(Path) : Path : Path;
    public bool PathFromDrive { get; set; }
    public Drive Drive { get; set; }
    public Dictionary<string, object?> Data { get; set; }

    public object? this[string key] {
        get => Get(key);
        set => Set(key, value);
    }

    public void Init() {
        if (PathFromDrive) {
            if (!Drive.DirExists(System.IO.Path.GetDirectoryName(Path) ?? "/")) return;
            if (!Drive.Exists(Path)) Drive.Create(Path);
        } else if (!System.IO.File.Exists(RelativePath)) {
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(RelativePath) ?? "/")) return;
            System.IO.File.Create(RelativePath);
        }
        Data = JsonSerializer.Deserialize<Dictionary<string, object?>>(System.IO.File.ReadAllText(RelativePath)) ?? new Dictionary<string, object?>();
        // Also add the data of 'this'
        Data[".nsb-db"] = Switch(this, "Data");
    }

    public static Dictionary<string, object?>? Switch(dynamic a, params string[] ignoreKeys) {
        Dictionary<string, object?> b = JsonSerializer.Deserialize<Dictionary<string, object?>>(JsonSerializer.Serialize(a, new JsonSerializerOptions() { WriteIndented = true })) ?? new Dictionary<string, object?>();
        foreach (string key in ignoreKeys) {
            if (b.ContainsKey(key)) b.Remove(key);
        }
        return b;
    }

    public Database(string path, bool pathFromDrive, Drive drive) {
        Path = path;
        PathFromDrive = pathFromDrive;
        Drive = drive;
        Data = new Dictionary<string, object?>();
        this.Init();
    }

    public void Set(string key, object? value, bool save = true) {
        Data[key] = value;
        if (save) Save();
    }

    public object? Get(string key) => Data[key];

    public void Save() {
        if (PathFromDrive) {
            Drive.Write(Path, JsonSerializer.Serialize(Data, new JsonSerializerOptions() { WriteIndented = true }));
        } else {
            System.IO.File.WriteAllText(RelativePath, JsonSerializer.Serialize(Data, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }

    public static object EmptyArray = new object[0];
}
