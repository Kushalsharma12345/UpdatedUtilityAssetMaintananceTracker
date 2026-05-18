using System.ComponentModel.DataAnnotations;

namespace AssetService.DTO
{
    public class CreateAssetDTO
    {
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Type { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Status { get; set; }

        public CreateLocationDTO? Location { get; set; }
    }
}