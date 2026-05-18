using AssetService.DTO;

namespace AssetService.Services
{
    public interface IAssetService
    {
        Task<AssetDTO?> CreateAssetAsync(CreateAssetDTO createAssetDTO);
        Task<AssetDTO?> GetAssetByIdAsync(int assetID);
        Task<IEnumerable<AssetDTO>> GetAllAssetsAsync();
        Task<AssetDTO?> UpdateAssetAsync(int assetID, CreateAssetDTO createAssetDTO);
        Task<IEnumerable<AssetDTO>> GetAssetsByBuildingAsync(string building);
    }
}