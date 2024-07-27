namespace InvestmentManagement.Domain.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(string name, string userName)
        {
            Name = name;
            UserName = userName;
        }

        public string Name { get; private set; }

        public string UserName { get; private set; }
    }
}

