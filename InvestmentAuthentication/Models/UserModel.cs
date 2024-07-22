using System.Text.RegularExpressions;

namespace InvestmentAuthentication.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(string userName, string name, string email, string password)
        {
            UserName = userName;
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private void Validate()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new Exception("UserName deve ser fornecido.");

            if (string.IsNullOrEmpty(Name))
                throw new Exception("Name deve ser fornecido.");

            if (string.IsNullOrEmpty(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("Email inválido.");

            if (string.IsNullOrEmpty(Password))
                throw new Exception("Password deve ser fornecido.");
        }
    }
}

