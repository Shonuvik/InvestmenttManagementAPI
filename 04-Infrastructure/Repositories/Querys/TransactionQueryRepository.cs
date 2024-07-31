using System.Text;
using Dapper;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Querys
{
    public class TransactionQueryRepository : ITransactionQueryRepository
    {
        private readonly IUnitOfWork _uow;
        public TransactionQueryRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<TransactionModel> GetTransactionByUserName(string asset, string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                              ");
            query.Append($"    a.ID AS {nameof(TransactionModel.AssetId)}       ");
            query.Append($"   ,t.ID AS {nameof(TransactionModel.TransactionId)} ");
            query.Append($"   ,p.ID AS {nameof(TransactionModel.PortfolioId)}   ");
            query.Append($"   ,a.NAME AS {nameof(TransactionModel.Symbol)}      ");
            query.Append($"   ,SUM(t.VALUE) AS {nameof(TransactionModel.Value)}      ");
            query.Append($"   ,t.QUANTITY AS {nameof(TransactionModel.Quantity)}");
            query.Append($"   ,a.UNIT_PRICE AS {nameof(TransactionModel.UnitPrice)} ");
            query.Append($" FROM [User] u                                       ");
            query.Append($" INNER JOIN [Portfolio] p on u.ID = p.USER_ID        ");
            query.Append($" INNER JOIN [Transaction] t on p.ID = t.PORTFOLIO_ID ");
            query.Append($" INNER JOIN [Asset] a on t.ASSET_ID = a.ID           ");
            query.Append($" WHERE u.USERNAME = @USERNAME                        ");
            query.Append($" AND a.NAME = @NAME                                  ");
            query.Append($" AND t.OPERATION_TYPE = 'V'                          ");
            query.Append($" GROUP BY                                            ");
            query.Append($"  a.ID                                               ");
            query.Append($" ,t.ID                                               ");
            query.Append($" ,p.ID                                               ");
            query.Append($" ,a.NAME                                             ");
            query.Append($" ,t.QUANTITY                                         ");
            query.Append($" ,a.UNIT_PRICE                                       ");

            using var connection = _uow.DbConnection;
            connection.Open();

            DynamicParameters parameters = new();
            parameters.Add("@USERNAME", userName);
            parameters.Add("@NAME", asset);

            var result = await connection.QueryFirstOrDefaultAsync<TransactionModel>(query.ToString(), parameters);
            return result;
        }
    }
}

