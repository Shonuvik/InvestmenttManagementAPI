using InvestmentManagement.Domain.Entities;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface IPortfolioCommandRepository
    {
        Task CreateNewPorfolioAsync(Portfolio porfolio);
    }
}

