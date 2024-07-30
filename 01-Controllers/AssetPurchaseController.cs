using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InvestmentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetPurchaseController : ControllerBase
    {
        private readonly IAssetPurchaseHandler _assetPurchaseHandler;
        public AssetPurchaseController(IAssetPurchaseHandler assetPurchaseHandler)
        {
            _assetPurchaseHandler = assetPurchaseHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssetPurchaseDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var name = value.GetName();

            await _assetPurchaseHandler.HandlerAsync(dto, name);

            return Ok();
        }
    }
}

