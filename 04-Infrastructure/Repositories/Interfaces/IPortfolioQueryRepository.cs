using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface IPortfolioQueryRepository
	{
        Task<PortfolioModel> GetAssetBySymbolAsync(string assetName, string userName);

        Task<long> GetPortfolioByUserName(string userName);
    }
}

