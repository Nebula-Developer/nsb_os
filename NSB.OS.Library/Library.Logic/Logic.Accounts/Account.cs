public class Account {
    public string Username { get; set; }
    public string Password { get; set; }
    public AccountOptionals Optionals { get; set; }

    public Account(string username, string password, AccountOptionals optionals) {
        Username = username;
        Password = password;
        Optionals = optionals;
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
