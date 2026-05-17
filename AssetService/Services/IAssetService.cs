using AssetService.DTO;
using AssetService.DTO;

namespace AssetService.Services
{
    public interface IAssetService
    {
        Task<AssetDTO> CreateAssetAsync(CreateAssetDTO createAssetDTO);
        Task<AssetDTO> GetAssetByIdAsync(int assetID);
        Task<IEnumerable<AssetDTO>> GetAllAssetsAsync();
    }
}