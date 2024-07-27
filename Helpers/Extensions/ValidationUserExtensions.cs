using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;

namespace InvestmentManagement.Helpers.Extensions
{
    public static class ValidationUserExtensions
    {
        public static string GetName(this StringValues value)
        {
            if (!value.Any())
                throw new Exception("Usuário inválido");

            var handler = new JwtSecurityTokenHandler();
            var token = value.ToString().Split(" ")[1];
            var claims = handler.ReadToken(token) as JwtSecurityToken;
            return claims?.Claims?.FirstOrDefault(x => x.Type == "unique_name").Value ?? string.Empty;
        }
    }
}

