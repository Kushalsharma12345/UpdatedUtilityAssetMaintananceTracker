using System.ComponentModel.DataAnnotations;

namespace AssetService.DTO
{
    public class CreateAssetDTO
    {
        [Required(ErrorMessage = "Asset name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Asset type is required")]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required(ErrorMessage = "Installation date is required")]
        public DateTime InstallationDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [MaxLength(50)]
        public string Status { get; set; }
    }
}