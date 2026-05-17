using AssetService.Models;

namespace AssetService.Repository
{
    public interface IAssetRepository
    {
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<Asset> GetAssetByIdAsync(int assetID);
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
    }
}