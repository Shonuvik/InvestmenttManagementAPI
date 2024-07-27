using System;
using System.Data;
using System.Data.SqlClient;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;

namespace InvestmentManagement.Infrastructure.DbContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _ConnectionString;
        public UnitOfWork(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public IDbConnection DbConnection => new SqlConnection(_ConnectionString);
    }
}

