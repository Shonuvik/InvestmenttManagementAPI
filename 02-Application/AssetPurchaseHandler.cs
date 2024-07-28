using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Infrastructure.Repositories.Querys;

namespace InvestmentManagement.Application
{
    public class AssetPurchaseHandler : IAssetPurchaseHandler
    {
        private readonly UserQueryRepository _user;
        public AssetPurchaseHandler(UserQueryRepository user)
        {
            _user = user;
        }

        public async Task AssetPurchaseAsync(AssetPurchaseDto dto, string userName)
        {
            var user = await _user.GetUserByUserName(userName) ?? throw new Exception("Usuário inválido");
        }
    }
}

