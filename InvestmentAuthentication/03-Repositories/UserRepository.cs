using System.Data.SqlClient;
using System.Text;
using Dapper;
using InvestmentAuthentication.Models;
using InvestmentAuthentication.Repositories.Interfaces;

namespace InvestmentAuthentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            StringBuilder query = new();
            query.Append($" SELECT ");
            query.Append($"      ID AS {nameof(UserModel.Id)} ");
            query.Append($"     ,USERNAME AS {nameof(UserModel.UserName)} ");
            query.Append($"     ,NAME AS {nameof(UserModel.Name)} ");
            query.Append($"     ,EMAIL AS {nameof(UserModel.Email)} ");
            query.Append($"     ,PASSWORD AS {nameof(UserModel.Password)} ");
            query.Append($"     ,CREATEDAT AS {nameof(UserModel.CreatedAt)} ");
            query.Append($" FROM [User]");
            query.Append($" WHERE USERNAME = @UserName");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName", userName);

            using var dbConnection = new SqlConnection(_configuration["ConnectionStrings"]);
            dbConnection.Open();

            var result = await dbConnection.QueryFirstOrDefaultAsync<UserModel>(query.ToString()
                , parameters);

            return result;
        }

        public async Task NewUserAsync(UserModel user)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO [User]    ");
            query.Append($"            (USERNAME, ");
            query.Append($"            NAME,      ");
            query.Append($"            EMAIL,     ");
            query.Append($"            PASSWORD,  ");
            query.Append($"            CREATEDAT) ");
            query.Append($"        VALUES         ");
            query.Append($"        (              ");
            query.Append($"            @USERNAME, ");
            query.Append($"            @NAME,     ");
            query.Append($"            @EMAIL,    ");
            query.Append($"            @PASSWORD, ");
            query.Append($"            @CREATEDAT ");
            query.Append($"        );             ");

            DynamicParameters parameters = new();

            parameters.Add("@USERNAME", user.UserName);
            parameters.Add("@NAME", user.Name);
            parameters.Add("@EMAIL", user.Email);
            parameters.Add("@PASSWORD", user.Password);
            parameters.Add("@CREATEDAT", user.CreatedAt);

            using var dbConnection = new SqlConnection(_configuration["ConnectionStrings"]);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(query.ToString(), parameters);
        }
    }
}

