using System.Text;
using Dapper;
using InvestmentManagement.Domain.Entities;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;

namespace InvestmentManagement.Infrastructure.Repositories.Commands
{
    public class TransactionCommandRepository : ITransactionCommandRepository
    {
        private readonly IUnitOfWork _uow;
        public TransactionCommandRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> CreateNewTransactionAsync(Transaction transaction)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO[Transaction]        ");
            query.Append($"         (                       ");
            query.Append($"             PORTFOLIO_ID,       ");
            query.Append($"             ASSET_ID,           ");
            query.Append($"             OPERATION_TYPE,     ");
            query.Append($"             QUANTITY,           ");
            query.Append($"             UNIT_PRICE,         ");
            query.Append($"             VALUE,              ");
            query.Append($"             TRANSACTION_DATE    ");
            query.Append($"         )                       ");
            query.Append($"         VALUES                  ");
            query.Append($"         (                       ");
            query.Append($"             @PORTFOLIO_ID,      ");
            query.Append($"             @ASSET_ID,          ");
            query.Append($"             @OPERATION_TYPE,    ");
            query.Append($"             @QUANTITY,          ");
            query.Append($"             @UNIT_PRICE,        ");
            query.Append($"             @VALUE,             ");
            query.Append($"             @TRANSACTION_DATE   ");
            query.Append($"         );                      ");

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = await connection.ExecuteAsync(query.ToString(), new
            {
                PORTFOLIO_ID = transaction.PortfolioId,
                ASSET_ID = transaction.AssetId,
                OPERATION_TYPE = transaction.TransactionType,
                QUANTITY = transaction.Quantity,
                UNIT_PRICE = transaction.UnitPrice,
                VALUE = transaction.Value,
                TRANSACTION_DATE = transaction.TransactionDate
            });

            return result != 0;
        }
    }
}

