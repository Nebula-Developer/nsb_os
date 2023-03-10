using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;
using NSB.OS.FileSystem;
using System.Text.Json;

namespace NSB.OS.Logic.AccountsNS;

public static class Accounts {
    public static Database Database = new Database("System/Private/accdb.njson", true, SystemDrives.BootDrive);
    public static List<Account>? GetAccounts => Database.Cast<List<Account>>("Accounts");
    public static Account? CurrentAccount = null;

    public static void Init() {
        Database = new Database("System/Private/accdb.njson", true, SystemDrives.BootDrive);
        if (Database["Accounts"] == null) Database["Accounts"] = Database.EmptyArray;
    }

    public static void AddAccount(Account account) {
        List<Account> accounts = GetAccounts ?? new List<Account>();
        accounts.Add(account);
        Database["Accounts"] = accounts;
        Database.Save();
    }

    public static void RemoveAccount(Account account) {
        List<Account> accounts = GetAccounts ?? new List<Account>();
        accounts.Remove(account);
        Database["Accounts"] = accounts;
        Database.Save();
    }

    public static void SetCurrentAccount(Account account) {
        CurrentAccount = account;
    }

    /// Example:
    /// GetAccount(new { Username = "admin", Password = "admin" })
    public static Account? GetAccount(object match) {
        List<Account> accounts = GetAccounts ?? new List<Account>();
        foreach (Account account in accounts) {
            if (account.Match(match)) return account;
        }
        return null;
    }
}
