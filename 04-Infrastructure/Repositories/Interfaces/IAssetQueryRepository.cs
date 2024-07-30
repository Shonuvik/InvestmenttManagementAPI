using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface IAssetQueryRepository
    {
        Task<AssetModel> GetAssetBySymbolAsync(string assetName);
    }
}

