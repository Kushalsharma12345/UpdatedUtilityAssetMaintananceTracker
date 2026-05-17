using System.ComponentModel.DataAnnotations;
namespace AssetService.DTO
{
    public class CreateAssetDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        [MaxLength(200)]
        public string Type { get; set; }
        [Required(ErrorMessage="Installation Date is required")]
        public DateTime InstallationDate { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(50)]
        public string Status { get; set; }
    }
}
