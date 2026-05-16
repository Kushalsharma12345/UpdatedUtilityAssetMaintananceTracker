namespace MaintenanceService1.Models;

public class MaintenancePlan
{
    public int PlanId { get; set; }
    public int AssetId { get; set; }
    public string Frequency { get; set; } = string.Empty;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}