using System.ComponentModel.DataAnnotations;

namespace WorkOrdersService.Models
{
    public class WorkLog
    {
        [Key]
        public int LogID { get; set; }

        public int WorkOrderID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TechnicianID { get; set; }
    }
}
