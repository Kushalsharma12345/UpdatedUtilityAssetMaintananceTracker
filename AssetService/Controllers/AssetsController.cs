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

        /// <summary>
        /// POST /api/assets - Register a new asset
        /// </summary>
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
                return CreatedAtAction(nameof(GetAssetById), new { id = assetDTO?.AssetID }, assetDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// GET /api/assets - List all assets
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                var assets = await _assetService.GetAllAssetsAsync();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// GET /api/assets/{id} - Get asset by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AssetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssetById(int id)
        {
            try
            {
                var assetDTO = await _assetService.GetAssetByIdAsync(id);
                if (assetDTO == null)
                {
                    return NotFound(new { message = "Asset not found" });
                }

                return Ok(assetDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// PUT /api/assets/{id} - Update asset details
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AssetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] CreateAssetDTO createAssetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var assetDTO = await _assetService.UpdateAssetAsync(id, createAssetDTO);
                if (assetDTO == null)
                {
                    return NotFound(new { message = "Asset not found" });
                }

                return Ok(assetDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// GET /api/assets/location?building={building} - Filter assets by building
        /// </summary>
        [HttpGet("location")]
        [ProducesResponseType(typeof(IEnumerable<AssetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssetsByBuilding([FromQuery] string building)
        {
            if (string.IsNullOrWhiteSpace(building))
            {
                return BadRequest(new { message = "Building parameter is required" });
            }

            try
            {
                var assets = await _assetService.GetAssetsByBuildingAsync(building);
                return Ok(assets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}