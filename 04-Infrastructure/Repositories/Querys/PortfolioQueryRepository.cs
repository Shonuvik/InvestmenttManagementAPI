using System.Text;
using Dapper;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Querys
{
    public class PortfolioQueryRepository : IPortfolioQueryRepository
    {
        private readonly IUnitOfWork _uow;
        public PortfolioQueryRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PortfolioModel> GetAssetBySymbolAsync(string assetName, string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                          ");
            query.Append($"      u.ID AS UserId,                            ");
            query.Append($"      p.Id AS PortfolioId                        ");
            query.Append($" FROM[User] u                                    ");
            query.Append($" INNER JOIN[Portfolio] p on u.ID = p.User_Id     ");
            query.Append($" INNER JOIN[AssetType] at on p.TYPE_ID = at.Id   ");
            query.Append($" WHERE u.USERNAME = @USERNAME                    ");
            query.Append($" AND at.NAME = @ASSETNAME                        ");

            DynamicParameters parameters = new();
            parameters.Add("@USERNAME", userName);
            parameters.Add("@ASSETNAME", assetName);

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<PortfolioModel>(query.ToString(), parameters);

            return result;
        }
    }
}

