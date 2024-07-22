using InvestmentAuthentication.Commands;
using InvestmentAuthentication.Controllers.V1.Dtos;
using InvestmentAuthentication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> NewUser(UserDto dto)
        {
            var result = await _userService.AddNewUserAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

