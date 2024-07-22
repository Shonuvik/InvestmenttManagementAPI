using InvestmentAuthentication.Models;
using InvestmentAuthentication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Token(CredentialModel model)
        {
            return Ok(await _authService.AuthAsync(model));
        }
    }
}

