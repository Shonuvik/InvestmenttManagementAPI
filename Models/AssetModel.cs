namespace InvestmentManagement.Models
{
    public class AssetModel
    {
        public long AssetId { get; set; }

        public long AssetTypeId { get; set; }

        public string AssetName { get; set; }

        public string AssetTypeName { get; set; }

        public string AssetTypeDescription { get; set; }

        public string Symbol { get; set; }

        public decimal UnitPrice { get; set; }
    }
}

