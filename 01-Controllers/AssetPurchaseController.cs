using InvestmentManagement.Controllers.V1.Dtos;
using InvestmentManagement.Helpers.Extensions;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InvestmentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetPurchaseController : ControllerBase
    {
        public AssetPurchaseController()
        {
        }

        [HttpPost]
        public async Task Post(AssetPurchaseDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var name = value.GetName();
        }
    }
}

