using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Helpers.Extensions;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InvestmentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioManagementController : ControllerBase
    {
        private readonly IAssetPurchaseHandler _assetPurchaseHandler;
        private readonly IAssetSellHandler _assetSellHandler;
        private readonly IPortfolioQueryRepository _portfolioQueryRepository;

        public PortfolioManagementController(IAssetPurchaseHandler assetPurchaseHandler,
                                             IAssetSellHandler assetSellHandler,
                                             IPortfolioQueryRepository portfolioQueryRepository)
        {
            _assetPurchaseHandler = assetPurchaseHandler;
            _assetSellHandler = assetSellHandler;
            _portfolioQueryRepository = portfolioQueryRepository;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost("BuyOrder")]
        public async Task<IActionResult> BuyOrder(AssetPurchaseDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var name = value.GetName();

            await _assetPurchaseHandler.HandlerAsync(dto, name);

            return Ok("Transação de compra realizada com sucesso.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost("SellOrder")]
        public async Task<IActionResult> SellOrder(AssetSellDto dto)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var name = value.GetName();

            await _assetSellHandler.HandlerAsync(dto, name);

            return Ok("Transação de venda realizada com sucesso.");
        }

        [ProducesResponseType(typeof(List<PortfolioModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("SearchPortfolio")]
        public async Task<IActionResult> SearchPortfolio()
        {
            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var name = value.GetName();

            var result = await _portfolioQueryRepository.GetPortfolioByUserName(name);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result.Select(x => new
            {
                Symbol = x.AssetName,
                x.Description,
                Portfolio = x.PortfolioName,
                x.Quantity,
                x.Value
            }));
        }
    }
}

