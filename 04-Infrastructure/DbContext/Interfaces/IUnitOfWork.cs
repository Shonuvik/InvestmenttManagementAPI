using System;
using System.Data;

namespace InvestmentManagement.Infrastructure.DbContext.Interfaces
{
    public interface IUnitOfWork
    {
        IDbConnection DbConnection { get; }
    }
}

