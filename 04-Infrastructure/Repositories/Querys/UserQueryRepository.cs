using System.Text;
using Dapper;
using InvestmentManagement.Domain.Entities;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;

namespace InvestmentManagement.Infrastructure.Repositories.Querys
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IUnitOfWork _uow;
        public UserQueryRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            StringBuilder query = new();
            query.Append($" SELECT ");
            query.Append($"      ID AS {nameof(User.Id)} ");
            query.Append($"     ,USERNAME AS {nameof(User.UserName)} ");
            query.Append($"     ,NAME AS {nameof(User.Name)} ");
            query.Append($" FROM [User]");
            query.Append($" WHERE USERNAME = @UserName");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName", userName);

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<User>(query.ToString()
                , parameters);

            return result;
        }
    }
}

