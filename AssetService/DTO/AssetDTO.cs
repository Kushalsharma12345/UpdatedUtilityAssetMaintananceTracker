namespace AssetService.DTO
{
    public class AssetDTO
    {
        public int AssetID { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public DateTime InstallationDate { get; set; }
        public string? Status { get; set; }
        public LocationDTO? Location { get; set; }
    }
}