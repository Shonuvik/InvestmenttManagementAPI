using System;
using InvestmentAuthentication.Services.Interfaces;

namespace InvestmentAuthentication.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message, object result = null)
        {
            Success = success;
            Message = message;
            Result = result;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}

