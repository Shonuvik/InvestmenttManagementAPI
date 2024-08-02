using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentManagement.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetQueryRepository _assetQuery;
        public AssetController(IAssetQueryRepository assetQuery)
        {
            _assetQuery = assetQuery;
        }

        //[Authorize]
        [ProducesResponseType(typeof(List<AssetModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetAsset(string assetName, string assetTypeName, string code)
        {
            var result = await _assetQuery.GetAssetAsync(assetName, assetTypeName, code);
            return result != null && result.Any()
                ? Ok(result.Select(x => new
                {
                    Asset = x.AssetName,
                    x.Symbol,
                    AssetType = x.AssetTypeName,
                    Price = x.UnitPrice
                }))
                : NotFound();
        }
    }
}

