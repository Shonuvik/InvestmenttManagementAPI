using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Domain.Entities;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;

namespace InvestmentManagement.Application
{
    public class AssetPurchaseHandler : IAssetPurchaseHandler
    {
        private readonly IUserQueryRepository _userQuery;
        private readonly IAssetQueryRepository _assetQuery;
        private readonly IPortfolioQueryRepository _portfolioQuery;
        private readonly IPortfolioCommandRepository _portfolioCommand;
        private readonly ITransactionCommandRepository _transactionCommand;

        public AssetPurchaseHandler(IUserQueryRepository userQuery,
                                    IAssetQueryRepository assetQuery,
                                    IPortfolioQueryRepository portfolioQuery,
                                    IPortfolioCommandRepository portfolioCommand,
                                    ITransactionCommandRepository transactionCommand)
        {
            _userQuery = userQuery;
            _assetQuery = assetQuery;
            _portfolioQuery = portfolioQuery;
            _portfolioCommand = portfolioCommand;
            _transactionCommand = transactionCommand;
        }

        public async Task HandlerAsync(AssetPurchaseDto dto, string userName)
        {
            var user = await _userQuery.GetUserByUserName(userName) ?? throw new Exception("Usuário inválido");

            if (string.IsNullOrEmpty(dto.Symbol))
                throw new Exception("Symbol nao pode ser vazio.");

            var asset = await _assetQuery.GetAssetBySymbolAsync(dto.Symbol)
                ?? throw new Exception("Nao foi encontrado o ativo informado.");

            var portfolio = await _portfolioQuery.GetAssetBySymbolAsync(asset.AssetName, userName);

            var portfolioEntity = ParseToPortfolio(asset, user);

            if (portfolio == null)
                await _portfolioCommand.CreateNewPorfolioAsync(portfolioEntity);

            var portfolioId = await _portfolioQuery.GetPortfolioIdByUserName(userName);

            var transactionEntity = ParseToTransaction(portfolioId, asset, dto);

            transactionEntity.CalculatePurchaseValue();
            var transactionResult = await _transactionCommand.CreateNewTransactionAsync(transactionEntity);

            if (!transactionResult)
                throw new Exception("Falha ao tentar persistir uma nova transacao");
        }

        private Portfolio ParseToPortfolio(AssetModel asset, User user)
        {
            return new Portfolio(user.Id, asset.AssetTypeId, asset.AssetTypeName, asset.AssetTypeDescription);
        }

        private Transaction ParseToTransaction(long portfolioId, AssetModel asset, AssetPurchaseDto assetPurchaseDto)
        {
            return new Transaction(portfolioId,
                                   asset.AssetId,
                                   TransactionType.C,
                                   assetPurchaseDto.Quantity,
                                   asset.UnitPrice
                                   );
        }
    }
}

