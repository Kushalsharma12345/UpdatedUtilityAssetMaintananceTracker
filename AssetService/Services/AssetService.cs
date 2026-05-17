using AssetService.DTO;
using AssetService.DTO;
using AssetService.Models;
using AssetService.Repository;
using AssetService.Repository;

namespace AssetService.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<AssetDTO> CreateAssetAsync(CreateAssetDTO createAssetDTO)
        {
            var asset = new Asset
            {
                Name = createAssetDTO.Name,
                Type = createAssetDTO.Type,
                InstallationDate = createAssetDTO.InstallationDate,
                Status = createAssetDTO.Status
            };

            var createdAsset = await _assetRepository.CreateAssetAsync(asset);

            return MapToDTO(createdAsset);
        }

        public async Task<AssetDTO> GetAssetByIdAsync(int assetID)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetID);
            return asset != null ? MapToDTO(asset) : null;
        }

        public async Task<IEnumerable<AssetDTO>> GetAllAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAssetsAsync();
            return assets.Select(MapToDTO).ToList();
        }

        private AssetDTO MapToDTO(Asset asset)
        {
            return new AssetDTO
            {
                AssetID = asset.AssetID,
                Name = asset.Name,
                Type = asset.Type,
                InstallationDate = asset.InstallationDate,
                Status = asset.Status
            };
        }
    }
}