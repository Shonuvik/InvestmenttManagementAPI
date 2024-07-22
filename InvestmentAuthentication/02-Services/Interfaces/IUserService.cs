using System;
using InvestmentAuthentication.Controllers.V1.Dtos;

namespace InvestmentAuthentication.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICommandResult> AddNewUserAsync(UserDto dto);
    }
}

