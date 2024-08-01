using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Interfaces
{
    public interface IAssetQueryRepository
    {
        Task<AssetModel> GetAssetBySymbolAsync(string assetName);
        Task<List<AssetModel>> GetAssetAsync(string assetName, string symbol, string code);
    }
}

