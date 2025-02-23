﻿using System;
namespace InvestmentManagement.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception innerException) : base(message, innerException) { }

        public override string ToString()
            => $"{Message} - Error: {InnerException}";
    }
}

