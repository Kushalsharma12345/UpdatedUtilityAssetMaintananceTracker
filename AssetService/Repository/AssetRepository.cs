using AssetService.Data;
using AssetService.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetService.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetDbContext _context;

        public AssetRepository(AssetDbContext context)
        {
            _context = context;
        }

        public async Task<Asset?> CreateAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset?> GetAssetByIdAsync(int assetID)
        {
            return await _context.Assets
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.AssetID == assetID);
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets
                .Include(a => a.Location)
                .ToListAsync();
        }

        public async Task<Asset?> UpdateAssetAsync(int assetID, Asset asset)
        {
            var existingAsset = await _context.Assets
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.AssetID == assetID);

            if (existingAsset == null)
                return null;

            existingAsset.Name = asset.Name;
            existingAsset.Type = asset.Type;
            existingAsset.InstallationDate = asset.InstallationDate;
            existingAsset.Status = asset.Status;

            _context.Assets.Update(existingAsset);
            await _context.SaveChangesAsync();

            return existingAsset;
        }

        public async Task<IEnumerable<Asset>> GetAssetsByBuildingAsync(string building)
        {
            return await _context.Assets
                .Include(a => a.Location)
                .Where(a => a.Location != null && a.Location.Building != null && a.Location.Building.ToLower() == building.ToLower())
                .ToListAsync();
        }

        public async Task<Location?> CreateLocationAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}