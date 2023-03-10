using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using NSB.OS.FileSystem;
using NSB.OS.Logic.Threads;

namespace NSB.OS.Runtime.ProgramsNS;

public class ProgramExecutable {
    public string name;
    public Assembly assembly;

    public ProgramExecutable(string name, Assembly assembly) {
        this.name = name;
        this.assembly = assembly;
    }
}

public static class Programs {
    public static List<ProgramExecutable> ListApps(Drive drive) {
        string searchPath = drive.GetPath("/Users/Shared/Programs");

        if (!Directory.Exists(searchPath)) {
            Directory.CreateDirectory(searchPath);
            return new List<ProgramExecutable>();
        }

        string[] assemblyFiles = Directory.GetFiles(searchPath, "*.dll");
        List<ProgramExecutable> programExecutables = new List<ProgramExecutable>();

        for (int i = 0; i < assemblyFiles.Length; i++) {
            string assemblyFile = assemblyFiles[i];
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            programExecutables.Add(new ProgramExecutable(assembly.GetName().Name ?? assembly.GetName().FullName, assembly));
        }

        return programExecutables;
    }

    public static ProgramReturn RunProgramExecutable(ProgramExecutable programExecutable) {
        ProgramReturn returns = new ProgramReturn(0, null);

        Type? programAType = programExecutable.assembly.GetType(programExecutable.assembly.GetName().Name + ".Program");
        MethodInfo? runMethod = programAType?.GetMethod("Run");

        if (programAType == null || runMethod == null) returns = new ProgramReturn(1, new Exception("Program: " + programExecutable.name + " does not have a Program.Run() method."));
        object? a;

        try {
            a = runMethod?.Invoke(null, null);
            returns = new ProgramReturn(0, null);
        } catch (TargetInvocationException e) {
            if (e.InnerException is TargetParameterCountException) {
                returns = new ProgramReturn(1, new Exception("Program: " + programExecutable.name + " does not have a Program.Run(CancellationToken) method."));
            } else {
                returns = new ProgramReturn(1, e.InnerException);
            }
        }

        return returns;
    }
}

public class ProgramReturn {
    public int exitCode;
    public Exception? exception;

    public ProgramReturn(int exitCode, Exception? exception) {
        this.exitCode = exitCode;
        this.exception = exception;
    }
}
