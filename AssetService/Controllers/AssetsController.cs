using AssetService.DTO;
using AssetService.DTO;
using AssetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AssetDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDTO createAssetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var assetDTO = await _assetService.CreateAssetAsync(createAssetDTO);
                return CreatedAtAction(nameof(CreateAsset), new { id = assetDTO.AssetID }, assetDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AssetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAssetById(int id)
        {
            var assetDTO = await _assetService.GetAssetByIdAsync(id);
            if (assetDTO == null)
            {
                return NotFound(new { message = "Asset not found" });
            }
            return Ok(assetDTO);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAssets()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return Ok(assets);
        }
    }
}