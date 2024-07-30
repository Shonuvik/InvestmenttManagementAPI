using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Dapper;
using InvestmentManagement.Domain.Entities;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;

namespace InvestmentManagement.Infrastructure.Repositories.Commands
{
    public class PortfolioCommandRepository : IPortfolioCommandRepository
    {
        private readonly IUnitOfWork _uow;

        public PortfolioCommandRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateNewPorfolioAsync(Portfolio porfolio)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO[Portfolio]            ");
            query.Append($"       (                           ");
            query.Append($"           USER_ID,                ");
            query.Append($"           TYPE_ID,                ");
            query.Append($"           NAME,                   ");
            query.Append($"           DESCRIPTION             ");
            query.Append($"       )                           ");
            query.Append($"        VALUES                     ");
            query.Append($"       (                           ");
            query.Append($"           @USER_ID,               ");
            query.Append($"           @TYPE_ID,               ");
            query.Append($"           @NAME,                  ");
            query.Append($"           @DESCRIPTION            ");
            query.Append($"       );                          ");

            using var connection = _uow.DbConnection;
            connection.Open();

            await connection.ExecuteAsync(query.ToString(), new
            {
                @USER_ID = porfolio.UserId,
                @TYPE_ID = porfolio.AssetTypeId,
                @NAME = porfolio.PortfolioName,
                @DESCRIPTION = porfolio.PortfolioDescription
            });
        }
    }
}

