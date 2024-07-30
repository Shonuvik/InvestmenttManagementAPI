using InvestmentManagement.Domain.Exceptions;

namespace InvestmentManagement.Domain.Entities
{
    public class Portfolio : Entity
    {
        public Portfolio(long userId,
                         long assetTypeId,
                         string portfolioName,
                         string portfolioDescription)
        {
            UserId = userId;
            AssetTypeId = assetTypeId;
            PortfolioName = portfolioName;
            PortfolioDescription = portfolioDescription;

            Validate();
        }

        public long UserId { get; set; }

        public long AssetTypeId { get; set; }

        public string PortfolioName { get; set; }

        public string PortfolioDescription { get; set; }

        private void Validate()
        {
            if (UserId == default)
                throw new DomainException("Id do usuário inválido.");

            if (AssetTypeId == default)
                throw new DomainException("Id do tipo do ativo inválido.");

            if (string.IsNullOrEmpty(PortfolioName))
                throw new DomainException("Nome do portfolio nao informado");
        }
    }
}

