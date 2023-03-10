using System;
using System.IO;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        string appsDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppContext.BaseDirectory, "Apps");
        if (!Directory.Exists(appsDirectory))
        {
            Console.WriteLine("The 'Apps' directory does not exist.");
            return;
        }

        // Get the list of all DLL files in the Apps directory
        string[] assemblyFiles = Directory.GetFiles(appsDirectory, "*.dll");

        // Display the list of available apps to the user
        Console.WriteLine("Available apps:");
        for (int i = 0; i < assemblyFiles.Length; i++)
        {
            string assemblyFile = assemblyFiles[i];
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            Console.WriteLine($"{i + 1}: {assembly.GetName().Name}");
        }

        // Ask the user to select an app
        Console.Write("Enter the number of the app to run: ");
        if (!int.TryParse(Console.ReadLine(), out int appNumber) || appNumber < 1 || appNumber > assemblyFiles.Length)
        {
            Console.WriteLine("Invalid app number.");
            return;
        }

        // Load and execute the selected app's Program.Run() method
        string selectedAssemblyFile = assemblyFiles[appNumber - 1];
        Assembly selectedAssembly = Assembly.LoadFrom(selectedAssemblyFile);

        Type? programAType = selectedAssembly.GetType(selectedAssembly.GetName().Name + ".Program");
        MethodInfo? runMethod = programAType?.GetMethod("Run");
        if (runMethod != null) runMethod.Invoke(null, null);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
