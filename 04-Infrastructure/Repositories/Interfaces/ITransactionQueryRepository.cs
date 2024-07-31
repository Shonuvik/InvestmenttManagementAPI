using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionQueryRepository
	{
        Task<TransactionModel> GetTransactionByUserName(string asset, string userName);
    }
}

