using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvestmentAuthentication.Models;
using InvestmentAuthentication.Repositories.Interfaces;
using InvestmentAuthentication.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace InvestmentAuthentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> AuthAsync(CredentialModel credentialModel)
        {
            if (credentialModel == null)
                return null;

            var credential = await _userRepository.GetUserByUserName(credentialModel.UserName)
            ?? throw new Exception("Nao existe credenciais cadastradas com o user informado");

            if (!(credential.UserName == credentialModel.UserName && credential.Password == credentialModel.Password))
                return null;

            var jwtKey = _configuration["JWTKey"];
            var tokenKey = Encoding.UTF8.GetBytes(jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, credentialModel.UserName) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

