
using InvestmentManagement.Domain.Entities;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionCommandRepository
    {
        Task<bool> CreateNewTransactionAsync(Transaction transaction);
    }
}

