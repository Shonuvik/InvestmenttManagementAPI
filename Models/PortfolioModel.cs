namespace InvestmentManagement.Models
{
    public class PortfolioModel
    {
        public long UserId { get; set; }

        public long PorfolioId { get; set; }

        public string AssetName { get; set; }

        public string PortfolioName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Value { get; set; }
    }
}
