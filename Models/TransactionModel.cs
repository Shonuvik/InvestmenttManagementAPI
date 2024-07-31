namespace InvestmentManagement.Models
{
    public class TransactionModel
    {
        public long AssetId { get; set; }

        public long TransactionId { get; set; }

        public long PortfolioId { get; set; }

        public string Symbol { get; set; }

        public decimal Value { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}