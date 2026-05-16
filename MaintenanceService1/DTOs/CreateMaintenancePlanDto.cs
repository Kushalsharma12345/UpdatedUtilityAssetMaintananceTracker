namespace MaintenanceService1.DTOs;

public class CreateMaintenancePlanDto
{
    public int AssetId { get; set; }
    public string Frequency { get; set; } = string.Empty;

    public List<TaskDto> Tasks { get; set; } = new();
}

public class TaskDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double EstimatedHours { get; set; }
}