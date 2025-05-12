using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

public class User
{
    public Guid UserId { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string UserPassword { get; set; }


    

    public User(string firstName, string lastName, string userEmail, string userPassword)
    {

        if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("UserName cannot be null or empty.");

        if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("UserName cannot be null or empty.");

        if (string.IsNullOrEmpty(userEmail) || !Regex.IsMatch(userEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))

            throw new ArgumentException("Invalid email format.");

        if (string.IsNullOrEmpty(userPassword)) throw new ArgumentException("Password cannot be null or empty.");

        UserId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserName = CreateUserName(firstName, lastName);
        UserEmail = userEmail;
        UserPassword = userPassword;

    }

    private string CreateUserName(string firstName, string lastName)
    {

        char firstInitial = char.ToLower(firstName[0]);

        string cleanLastName = lastName.Trim().ToLower();

        Random random = new Random();

        int randomNumber = random.Next(1000, 10000);

        string userName = firstInitial + cleanLastName + randomNumber;

        return userName;
    }

}