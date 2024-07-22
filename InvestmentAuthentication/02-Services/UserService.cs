using System;
using InvestmentAuthentication.Commands;
using InvestmentAuthentication.Controllers.V1.Dtos;
using InvestmentAuthentication.Models;
using InvestmentAuthentication.Repositories.Interfaces;
using InvestmentAuthentication.Services.Interfaces;

namespace InvestmentAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> AddNewUserAsync(UserDto dto)
        {
            if (dto == null)
                return new CommandResult(false, "Objeto inválido.");

            var model = ParseToModel(dto);

            var user = await _userRepository.GetUserByUserName(model.UserName);

            if (user != null)
                return new CommandResult(false, "O UserName informado já existe.");

            await _userRepository.NewUserAsync(model);

            return new CommandResult(true, "Usuário criado com sucesso",
                new
                {
                    model.UserName,
                    model.Name,
                    model.Email
                });
        }

        private UserModel ParseToModel(UserDto userDto)
        {
            return new UserModel(userDto.UserName, userDto.Name, userDto.Email, userDto.Password);
        }
    }
}

