using System.Text;
using Dapper;
using InvestmentManagement.Infrastructure.DbContext.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Models;

namespace InvestmentManagement.Infrastructure.Repositories.Querys
{
    public class AssetQueryRepository : IAssetQueryRepository
    {
        private readonly IUnitOfWork _uow;
        public AssetQueryRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AssetModel> GetAssetBySymbolAsync(string assetName)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                                              ");
            query.Append($"      A.ID AS {nameof(AssetModel.AssetId)}                           ");
            query.Append($"     ,AT.ID AS {nameof(AssetModel.AssetTypeId)}                      ");
            query.Append($"     ,A.Name AS  {nameof(AssetModel.AssetName)}                      ");
            query.Append($"     ,AT.Name AS {nameof(AssetModel.AssetTypeName)}                  ");
            query.Append($"     ,AT.Description AS {nameof(AssetModel.AssetTypeDescription)}    ");
            query.Append($"     ,A.PRICE AS {nameof(AssetModel.UnitPrice)}                      ");
            query.Append($" FROM Asset A                                                        ");
            query.Append($" INNER JOIN AssetType AT ON A.TYPE_ID = AT.ID                        ");
            query.Append($" WHERE A.NAME = @NAME                                                ");

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<AssetModel>(query.ToString(),
                new
                {
                    NAME = assetName
                });

            return result;
        }

        public async Task<List<AssetModel>> GetAssetAsync(string assetName, string assetType, string code)
        {
            StringBuilder query = new();
            DynamicParameters parameters = new DynamicParameters();

            query.Append($"SELECT                                                ");
            query.Append($"     A.NAME AS {nameof(AssetModel.AssetName)}         ");
            query.Append($"    ,AT.NAME AS {nameof(AssetModel.AssetTypeName)}    ");
            query.Append($"    ,A.SYMBOL_CODE AS {nameof(AssetModel.Symbol)}     ");
            query.Append($"    ,A.PRICE AS {nameof(AssetModel.UnitPrice)}        ");
            query.Append($" FROM[Asset] A                                        ");
            query.Append($" INNER JOIN[AssetType] AT ON A.TYPE_ID = AT.ID        ");
            query.Append($" WHERE 1 = 1                                          ");

            if (assetName != null)
            {
                query.Append($" AND A.NAME = @NAME ");
                parameters.Add("@NAME", assetName);
            }

            if (code != null)
            {
                query.Append($" AND AT.NEGOTIATION_CODE = @NEGOTIATION_CODE");
            }

            if (assetType != null)
            {
                query.Append($" AND AT.NAME = @NAME ");
                parameters.Add("@NAME", assetType);
            }

            using var connection = _uow.DbConnection;
            connection.Open();

            var result = (await connection.QueryAsync<AssetModel>(query.ToString(), parameters)).ToList();

            return result;
        }
    }
}

