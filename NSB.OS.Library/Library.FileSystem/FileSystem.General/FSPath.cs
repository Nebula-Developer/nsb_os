using System;
using System.Reflection;

namespace NSB.OS.FileSystem;

public static partial class FSPath {
    public static string Root => Assembly.GetExecutingAssembly().Location ?? AppDomain.CurrentDomain.BaseDirectory;
    public static string Combine(params string[] paths) => System.IO.Path.Combine(paths);
    public static string GetRootPath(params string[] paths) => Combine(Root, Combine(paths));
}
