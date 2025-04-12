using System.Text.RegularExpressions;

public class User
{

    public Guid UserId { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }

    public DateTime BornDate { get; set; }

    public User(string firstName, string lastName, string userEmail, string password, DateTime bornDate)
    {

        if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("FirstName cannot be null or empty.");

        if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("LastName cannot be null or empty.");

        if (string.IsNullOrEmpty(userEmail) || !Regex.IsMatch(userEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))

            throw new ArgumentException("Invalid email format.");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty.");


        UserId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserName = CreateUserName(firstName, lastName);
        UserEmail = userEmail;
        Password = password;
        BornDate = bornDate;

    }

    private string CreateUserName(string firstName, string lastName)

    {

        var random = new Random();
        var userName = firstName.Substring(0, 1) + lastName + random.Next(1000, 10000);

        return userName;
    }

}