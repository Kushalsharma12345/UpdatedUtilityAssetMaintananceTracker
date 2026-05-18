using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetService.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required]
        public int AssetID { get; set; }

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

        [ForeignKey("AssetID")]
        public Asset? Asset { get; set; }
    }
}