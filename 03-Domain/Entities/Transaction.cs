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
                           decimal unitPrice)
        {
            PortfolioId = portfolioId;
            AssetId = assetId;
            TransactionType = transactionType;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TransactionDate = DateTime.Now;
        }

        public long PortfolioId { get; private set; }

        public long AssetId { get; private set; }

        public TransactionType TransactionType { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Value { get; private set; }

        public DateTime TransactionDate { get; private set; }


        public void CalculatePurchaseValue()
        {
            Value = UnitPrice * Quantity;
        }

        public void CalculateQuantityForSell()
        {
            Quantity = Convert.ToInt32(Value) / Convert.ToInt32(UnitPrice);
        }
    }
}
