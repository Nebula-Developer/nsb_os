using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NSB.OS.Runtime.SignalsNS;

public static class Signals {
    // Import the signal() function from the C standard library
    [DllImport("libc")]
    static extern IntPtr signal(int sig, IntPtr handler);

    // Define a delegate that matches the signature of the signal handler function
    delegate void SignalHandler(int sig);

    // Signal handler function
    static void sighandler(int sig) {
        // Reset the signal handler to catch the signal next time
        signal(sig, Marshal.GetFunctionPointerForDelegate(handler));

        Console.WriteLine("Cannot execute Ctrl+Z");
    }

    // Global variable to hold a reference to the signal handler delegate
    static SignalHandler handler;

    public static void CancelAll() {
        // Assign the signal handler delegate to the global variable
        handler = sighandler;

        // Set the SIGTSTP (Ctrl+Z) signal handler
        signal(SIGTSTP, Marshal.GetFunctionPointerForDelegate(handler));

        // Loop indefinitely
        while (true) {
        }
    }

    // Define the SIGTSTP signal number
    const int SIGTSTP = 20;
}