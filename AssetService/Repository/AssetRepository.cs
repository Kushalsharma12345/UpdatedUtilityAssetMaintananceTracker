using AssetService.Data;
using AssetService.Models;
using AssetService.Repository;
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

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset> GetAssetByIdAsync(int assetID)
        {
            return await _context.Assets
                .FirstOrDefaultAsync(a => a.AssetID == assetID);
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets.ToListAsync();
        }
    }
}