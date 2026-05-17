using AssetService.Data;
using AssetService.Models;
namespace AssetService.Repository
{
    public class AssetRepository:IAssetRepository
    {
        private readonly AssetDbContext _context;
        public  AssetRepository(AssetDbContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }
    }
}
