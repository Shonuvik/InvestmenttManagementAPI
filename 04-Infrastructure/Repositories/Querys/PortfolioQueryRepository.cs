﻿using System.Text;
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

        public async Task<long> GetPortfolioIdByUserName(string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                        ");
            query.Append($"      P.ID AS PortfolioId                      ");
            query.Append($" FROM[User] U                                  ");
            query.Append($" INNER JOIN[Portfolio] P ON U.ID = P.USER_ID   ");
            query.Append($" WHERE U.USERNAME = @USERNAME                  ");

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<long>(query.ToString(), new
            {
                USERNAME = userName
            });

            return result;
        }

        public async Task<List<PortfolioModel>> GetPortfolioByUserName(string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                        ");
            query.Append($"        a.NAME AS AssetName                    ");
            query.Append($"       ,p.NAME AS PortfolioName                ");
            query.Append($"       ,p.DESCRIPTION AS Description           ");
            query.Append($"       ,t.Quantity AS Quantity                 ");
            query.Append($"       ,t.VALUE AS VALUE                       ");
            query.Append($" FROM[User] u                                  ");
            query.Append($" INNER JOIN[Portfolio] p on u.ID = p.USER_ID   ");
            query.Append($" INNER JOIN[Transaction] t on p.ID = t.PORTFOLIO_ID ");
            query.Append($" INNER JOIN[Asset] a on t.ASSET_ID = a.ID      ");
            query.Append($" WHERE U.USERNAME = 'Marlon123'                ");

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = (await connection.QueryAsync<PortfolioModel>(query.ToString(), new
            {
                USERNAME = userName
            })).ToList();

            return result;
        }
    }
}

