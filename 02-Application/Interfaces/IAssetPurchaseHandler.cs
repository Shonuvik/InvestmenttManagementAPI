using InvestmentManagement.Controllers.V1.Dtos;

namespace InvestmentManagement.Application.Interfaces
{
    public interface IAssetPurchaseHandler
	{
        Task HandlerAsync(AssetPurchaseDto dto, string userName);
    }
}