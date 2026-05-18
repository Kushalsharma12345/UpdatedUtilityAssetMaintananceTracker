namespace AssetService.DTO
{
    public class LocationDTO
    {
        public int LocationID { get; set; }
        public int AssetID { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }
        public string? Zone { get; set; }
        public string? SiteCode { get; set; }
    }
}