using System;
namespace InvestmentAuthentication.Services.Interfaces
{
    public interface ICommandResult
    {
        bool Success { get; set; }

        string Message { get; set; }

        object Result { get; set; }
    }
}

