using AssetService.DTO;
using AssetService.Models;
using AssetService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        // POST: api/assets
        [HttpPost]
        public async Task<ActionResult<AssetDto>> CreateAsset([FromBody] CreateAssetDto createAssetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = new Asset
            {
                Name = createAssetDto.Name,
                Type = createAssetDto.Type,
                InstallationDate = createAssetDto.InstallationDate,
                Status = createAssetDto.Status
            };

            var createdAsset = await _assetRepository.CreateAssetAsync(asset);

            var assetDto = new AssetDto
            {
                AssetID = createdAsset.AssetID,
                Name = createdAsset.Name,
                Type = createdAsset.Type,
                InstallationDate = createdAsset.InstallationDate,
                Status = createdAsset.Status
            };

            return CreatedAtAction("CreateAsset", new { id = assetDto.AssetID }, assetDto);
        }
    }
}