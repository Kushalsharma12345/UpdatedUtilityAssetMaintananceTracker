namespace MaintenanceService1.Models;

public class TaskItem
{
    public int TaskId { get; set; }

    public int PlanId { get; set; }
    public MaintenancePlan Plan { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double EstimatedHours { get; set; }
}