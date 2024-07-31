using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Domain.Entities;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;

namespace InvestmentManagement.Application
{
    public class AssetSellHandler : IAssetSellHandler
    {
        private readonly IPortfolioQueryRepository _portfolioQuery;
        private readonly IUserQueryRepository _userQuery;
        private readonly ITransactionQueryRepository _transactionQuery;
        private readonly ITransactionCommandRepository _transactionCommand;

        public AssetSellHandler(IPortfolioQueryRepository portfolioQuery,
                                IUserQueryRepository userQuery,
                                ITransactionQueryRepository transactionQuery,
                                ITransactionCommandRepository transactionCommand)
        {
            _portfolioQuery = portfolioQuery;
            _userQuery = userQuery;
            _transactionQuery = transactionQuery;
            _transactionCommand = transactionCommand;
        }

        public async Task HandlerAsync(AssetSellDto dto, string userName)
        {
            var user = await _userQuery.GetUserByUserName(userName) ?? throw new Exception("Usuário inválido");

            var transaction = await _transactionQuery.GetTransactionByUserName(dto.Symbol, userName)
                ?? throw new Exception("Usuário não possui o ativo informado");

            if (transaction.Value < dto.Value || dto.Value < transaction.UnitPrice)
                throw new Exception("O Usuário possuí valor inferior ao solicitado ou o valor informado é menor que o preco unitario");

            var transactionEntity = ParseToTransaction(transaction);

            transactionEntity.CalculateQuantityForSell();
            var result = await _transactionCommand.CreateNewTransactionAsync(transactionEntity);

            if (!result)
                throw new Exception("Falha ao tentar persistir uma nova transacao");
        }

        private Transaction ParseToTransaction(TransactionModel transaction)
        {
            return new Transaction(transaction.PortfolioId,
                                   transaction.AssetId,
                                   TransactionType.V,
                                   transaction.Quantity,
                                   transaction.UnitPrice
                                   );
        }
    }
}

