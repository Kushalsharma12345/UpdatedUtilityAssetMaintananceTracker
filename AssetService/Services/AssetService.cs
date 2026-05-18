using AssetService.DTO;
using AssetService.Models;
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

        /// <summary>
        /// Create a new asset with location details
        /// </summary>
        public async Task<AssetDTO?> CreateAssetAsync(CreateAssetDTO createAssetDTO)
        {
            var asset = new Asset
            {
                Name = createAssetDTO.Name,
                Type = createAssetDTO.Type,
                InstallationDate = createAssetDTO.InstallationDate,
                Status = createAssetDTO.Status
            };

            var createdAsset = await _assetRepository.CreateAssetAsync(asset);

            // If location is provided, create it
            if (createAssetDTO.Location != null)
            {
                var location = new Location
                {
                    AssetID = createdAsset.AssetID,
                    Building = createAssetDTO.Location.Building,
                    Floor = createAssetDTO.Location.Floor,
                    Zone = createAssetDTO.Location.Zone,
                    SiteCode = createAssetDTO.Location.SiteCode
                };

                await _assetRepository.CreateLocationAsync(location);
                createdAsset.Location = location;
            }

            return MapToDTO(createdAsset);
        }

        /// <summary>
        /// Get asset by ID with location details
        /// </summary>
        public async Task<AssetDTO?> GetAssetByIdAsync(int assetID)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetID);
            return asset != null ? MapToDTO(asset) : null;
        }

        /// <summary>
        /// Get all assets with location details
        /// </summary>
        public async Task<IEnumerable<AssetDTO>> GetAllAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAssetsAsync();
            return assets.Select(MapToDTO).ToList();
        }

        /// <summary>
        /// Update asset details
        /// </summary>
        public async Task<AssetDTO?> UpdateAssetAsync(int assetID, CreateAssetDTO createAssetDTO)
        {
            var asset = new Asset
            {
                Name = createAssetDTO.Name,
                Type = createAssetDTO.Type,
                InstallationDate = createAssetDTO.InstallationDate,
                Status = createAssetDTO.Status
            };

            var updatedAsset = await _assetRepository.UpdateAssetAsync(assetID, asset);

            if (updatedAsset == null)
                return null;

            return MapToDTO(updatedAsset);
        }

        /// <summary>
        /// Get all assets in a specific building
        /// </summary>
        public async Task<IEnumerable<AssetDTO>> GetAssetsByBuildingAsync(string building)
        {
            var assets = await _assetRepository.GetAssetsByBuildingAsync(building);
            return assets.Select(MapToDTO).ToList();
        }

        /// <summary>
        /// Map Asset model to AssetDTO
        /// </summary>
        private AssetDTO MapToDTO(Asset asset)
        {
            return new AssetDTO
            {
                AssetID = asset.AssetID,
                Name = asset.Name,
                Type = asset.Type,
                InstallationDate = asset.InstallationDate,
                Status = asset.Status,
                Location = MapLocationToDTO(asset.Location)
            };
        }

        /// <summary>
        /// Map Location model to LocationDTO
        /// </summary>
        private LocationDTO? MapLocationToDTO(Location? location)
        {
            if (location == null)
                return null;

            return new LocationDTO
            {
                LocationID = location.LocationID,
                AssetID = location.AssetID,
                Building = location.Building,
                Floor = location.Floor,
                Zone = location.Zone,
                SiteCode = location.SiteCode
            };
        }
    }
}