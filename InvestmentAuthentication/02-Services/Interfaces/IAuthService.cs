using System;
using InvestmentAuthentication.Models;

namespace InvestmentAuthentication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthAsync(CredentialModel credentialModel);
    }
}

