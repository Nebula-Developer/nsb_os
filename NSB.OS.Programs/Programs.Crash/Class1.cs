using System;
using NSB.OS.Graphics.DisplayNS;
using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;

namespace Programs.Crash;

public static class Program {
    public static void Run() {
        Console.WriteLine("Use throw method or divide by zero method?");
        Console.WriteLine("1. Throw method");
        Console.WriteLine("2. Divide by zero method");
        Console.WriteLine("3. Exit");
        Console.Write("Choice: ");
        
        CHOICE:
        string choice = Console.ReadLine();
        switch (choice) {
            case "1":
                throw new Exception("This is a fake error to test the error handler.");
                break;
            case "2":
                int a = 1;
                int b = 0;
                int c = a / b;
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                goto CHOICE;
        }
    }
}
