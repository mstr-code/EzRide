using System;
using System.Text.RegularExpressions;

namespace EzRide.Core.Domain
{
    public class User
    {
        // private string firstName;
        // public string middleName;
        // public string lastName;
        // User's unique email address.
        // Can be changed if needed.
        private string email;
        // User's unique username.
        // Used for login purposes.
        public string username;
        // User's salted password.
        private string password;
        
        private static readonly Regex RegexName = new Regex("[a-zA-Z]");

        // User's unique identifier.
        public Guid Id { get; protected set; }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new System.Exception("Please provide a valid Email address.");
                if (email == value.ToLowerInvariant())
                    return;
                email = value.ToLowerInvariant();
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public string Username
        {
            get => username;
            set
            {
                if (!RegexName.IsMatch(value))
                    throw new System.Exception("Please provide a valid Username.");
                if (username == value)
                    return;
                username = value;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new System.Exception("Please provide a valid Password.");
                if (password == value)
                    return;
                password = value;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public string Salt { get; protected set; }

        // User's role (either regular 'user' or 'admin').
        public string Role { get; set; }

        public DateTime UpdatedAt { get; protected set; }
        
        protected User() { }

        public User(Guid id, string email, string username, string password, string salt,  string role)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            Username = username;
            Password = password;
            Salt = salt;
            Role  = role;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}