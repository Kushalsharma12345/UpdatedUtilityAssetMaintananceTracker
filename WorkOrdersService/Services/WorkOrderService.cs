using Microsoft.EntityFrameworkCore;
using WorkOrdersService.Data;
using WorkOrdersService.DTOs;
using WorkOrdersService.Models;

namespace WorkOrdersService.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly AppDbContext _context;

        public WorkOrderService(AppDbContext context)
        {
            _context = context;
        }

        // Create new work order
        public async Task<WorkOrder> CreateWorkOrder(WorkOrderDto dto)
        {
            var workOrder = new WorkOrder
            {
                PlanID = dto.PlanID,
                ScheduledDate = dto.ScheduledDate,
                Status = "Open"
            };

            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();

            return workOrder;
        }

        // Get work orders by status
        public async Task<List<WorkOrder>> GetWorkOrders(string status)
        {
            return await _context.WorkOrders
                                 .Where(w => w.Status == status)
                                 .ToListAsync();
        }

        // Update work order status
        public async Task<bool> UpdateStatus(int id, string status)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);

            if (workOrder == null)
                return false;

            workOrder.Status = status;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}