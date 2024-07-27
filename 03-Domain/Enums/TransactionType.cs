using System.ComponentModel;

namespace InvestmentManagement.Domain.Entities
{
    public enum TransactionType
    {
        [Description("Compra")]
        C,

        [Description("Venda")]
        V
    }
}

