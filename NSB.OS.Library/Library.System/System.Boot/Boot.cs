using System.Reflection;
using NSB.OS.FileSystem;
using NSB.OS.Logic.AccountsNS;
using NSB.OS.SystemNS.InputNS;

namespace NSB.OS.SystemNS.BootNS;

public static partial class Boot {
    public static void Init() {
        // Init the boot drive
        SystemDrives.Init();

        // Make sure the boot drive is initialized
        if (!FSInit.CheckInitialized(SystemDrives.BootDrive, true)) FSInit.Initialize(SystemDrives.BootDrive);
        if (!SystemDrives.BootDrive.Exists("/Users/Shared/Programs")) SystemDrives.BootDrive.CreateDir("/Users/Shared/Programs");

        // Init the accounts database
        Accounts.Init();

        // Make sure an Administrator account exists
        if (Accounts.GetAccount(new { Username = "admin" }) == null) {
            Accounts.AddAccount(new Account("admin", "admin", new AccountOptionals() {
                FirstName = "Administrator"
            }));
        }
        
        Input.Start();
    }
}
