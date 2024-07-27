using System;
namespace InvestmentManagement.Domain.Entities
{
    public class Transaction : Entity
    {
        public Transaction() { }

        public Transaction(long portfolioId,
                           long assetId,
                           TransactionType transactionType,
                           int quantity,
                           decimal unitPrice,
                           decimal value,
                           DateTime transactionDate)
        {
            PortfolioId = portfolioId;
            AssetId = assetId;
            TransactionType = transactionType;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Value = value;
            TransactionDate = transactionDate;
        }

        public long PortfolioId { get; set; }

        public long AssetId { get; set; }

        public TransactionType TransactionType { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Value { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
