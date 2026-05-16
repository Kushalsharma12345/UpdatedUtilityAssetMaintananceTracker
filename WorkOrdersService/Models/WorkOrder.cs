using System.ComponentModel.DataAnnotations;

namespace WorkOrdersService.Models
{
    public class WorkOrder
    {
        [Key]
        public int WorkOrderID { get; set; }

        public int PlanID { get; set; }

        public DateTime ScheduledDate { get; set; }

        // Open | InProgress | Completed
        public string Status { get; set; } = "Open";
    }
}