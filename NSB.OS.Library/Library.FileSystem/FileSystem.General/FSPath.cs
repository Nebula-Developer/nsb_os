using System;
using System.Reflection;

namespace NSB.OS.FileSystem;

public static partial class FSPath {
    public static string Root => System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
    public static string Combine(params string[] paths) => System.IO.Path.Combine(paths);
    public static string GetRootPath(params string[] paths) => Combine(Root, Combine(paths));
}
