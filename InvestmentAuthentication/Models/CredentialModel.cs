namespace InvestmentAuthentication.Models
{
    public class CredentialModel
    {
        public CredentialModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }
    }
}

