using System;
using System.IO;
using System.Reflection;

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
    public static List<ProgramExecutable> ListApps(string? searchPath = null) {
        if (searchPath == null) searchPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Apps");;
    
        if (!Directory.Exists(searchPath)) throw new Exception("Directory: " + searchPath + " does not exist.");

        string[] assemblyFiles = Directory.GetFiles(searchPath, "*.dll");
        List<ProgramExecutable> programExecutables = new List<ProgramExecutable>();

        for (int i = 0; i < assemblyFiles.Length; i++)
        {
            string assemblyFile = assemblyFiles[i];
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            programExecutables.Add(new ProgramExecutable(assembly.GetName().Name ?? assembly.GetName().FullName, assembly));
        }

        return programExecutables;
    }

    public static int RunProgramExecutable(ProgramExecutable programExecutable) {
        Type? programAType = programExecutable.assembly.GetType(programExecutable.assembly.GetName().Name + ".Program");
        MethodInfo? runMethod = programAType?.GetMethod("Run");
        
        if (programAType == null || runMethod == null) return -1;

        try {
            runMethod.Invoke(null, null);
            return 0;
        } catch {
            return -1;
        }
    }
}
