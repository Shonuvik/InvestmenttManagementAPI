using System;
using InvestmentAuthentication.Models;

namespace InvestmentAuthentication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByUserName(string userName);
        Task NewUserAsync(UserModel user);
    }
}

