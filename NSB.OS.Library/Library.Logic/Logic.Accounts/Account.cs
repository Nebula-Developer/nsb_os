using System.Reflection;

public class Account {
    public string Username { get; set; }
    public string Password { get; set; }
    public AccountOptionals Optionals { get; set; }

    public Account(string username, string password, AccountOptionals optionals) {
        Username = username;
        Password = password;
        Optionals = optionals;
    }

    public bool Match(object match) {
        foreach (PropertyInfo property in match.GetType().GetProperties()) {
            if (property.GetValue(match) != null) {
                var value = property.GetValue(match);
                if (value != null && value.ToString() != this.GetType().GetProperty(property.Name)?.GetValue(this)?.ToString()) return false;
            }
        }
        return true;
    }
}

public class AccountOptionals {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public string? Country { get; set; }
}
