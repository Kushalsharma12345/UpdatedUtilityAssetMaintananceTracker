using System.ComponentModel.DataAnnotations;

namespace AssetService.DTO
{
    public class CreateLocationDTO
    {
        [Required]
        [MaxLength(150)]
        public string? Building { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Floor { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Zone { get; set; }

        [Required]
        [MaxLength(50)]
        public string? SiteCode { get; set; }
    }
}