using InvestmentManagement.Domain.Entities;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface IUserQueryRepository
    {
        Task<User> GetUserByUserName(string userName);
    }
}

