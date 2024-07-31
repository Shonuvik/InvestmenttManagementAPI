using InvestmentManagement.Controllers.V1.Dtos;

namespace InvestmentManagement.Application.Interfaces
{
    public interface IAssetSellHandler
    {
        Task HandlerAsync(AssetSellDto dto, string userName);
    }
}

